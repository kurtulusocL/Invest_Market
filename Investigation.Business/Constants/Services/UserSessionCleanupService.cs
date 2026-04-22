using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Investigation.Business.Constants.Services
{
    public class UserSessionCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserSessionCleanupService> _logger;

        public UserSessionCleanupService(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<UserSessionCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRun = DateTime.Today.AddDays(now.Hour >= 3 ? 1 : 0).AddHours(3);
                var delay = nextRun - now;

                _logger.LogInformation("UserSession cleanup bir sonraki çalışma: {NextRun}", nextRun);

                await Task.Delay(delay, stoppingToken);
                await CleanupAsync(stoppingToken);
            }
        }

        private async Task CleanupAsync(CancellationToken stoppingToken)
        {
            try
            {
                var retentionDays = _configuration.GetValue<int>("UserSessionSettings:RetentionDays", 90);
                var cutoffDate = DateTime.UtcNow.AddDays(-retentionDays);

                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var updated = await context.UserSessions.Where(s => s.CreatedDate < cutoffDate && s.IsDeleted == false)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsDeleted, true).SetProperty(x => x.DeletedDate, DateTime.UtcNow), stoppingToken);

                _logger.LogInformation("UserSession cleanup tamamlandı: {Count} kayıt soft delete edildi. Retention: {Days} gün. Cutoff: {Cutoff}", updated, retentionDays, cutoffDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserSession cleanup sırasında hata oluştu.");
            }
        }
    }
}
