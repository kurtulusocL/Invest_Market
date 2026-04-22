using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Investigation.Business.Constants.Services
{
    public class HeartbeatCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<HeartbeatCleanupService> _logger;

        public HeartbeatCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<HeartbeatCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
                await CleanupAsync(stoppingToken);
            }
        }

        private async Task CleanupAsync(CancellationToken stoppingToken)
        {
            try
            {
                var cutoff = DateTime.UtcNow.AddMinutes(-2);

                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var updated = await context.UserSessions
                    .Where(s => s.IsOnline &&
                                s.IsActive &&
                                !s.IsDeleted &&
                                s.LastHeartbeat != null &&
                                s.LastHeartbeat < cutoff)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsOnline, false).SetProperty(x => x.LogoutDate, DateTime.UtcNow), stoppingToken);

                if (updated > 0)
                    _logger.LogInformation("Heartbeat cleanup: {Count} session offline işaretlendi.", updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Heartbeat cleanup sırasında hata oluştu.");
            }
        }
    }
}
