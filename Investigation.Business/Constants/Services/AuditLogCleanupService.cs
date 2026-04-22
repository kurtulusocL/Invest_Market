using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Investigation.Business.Constants.Services
{
    public class AuditLogCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuditLogCleanupService> _logger;

        public AuditLogCleanupService(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<AuditLogCleanupService> logger)
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

                _logger.LogInformation("AuditLog cleanup bir sonraki çalışma: {NextRun}", nextRun);

                await Task.Delay(delay, stoppingToken);
                await CleanupAsync(stoppingToken);
            }
        }

        private async Task CleanupAsync(CancellationToken stoppingToken)
        {
            try
            {
                var retentionDays = _configuration.GetValue<int>("AuditLogSettings:RetentionDays", 45);
                var cutoffDate = DateTime.UtcNow.AddDays(-retentionDays);

                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var deleted = await context.Audits.Where(a => a.CreatedDate < cutoffDate).ExecuteDeleteAsync(stoppingToken);

                _logger.LogInformation("AuditLog cleanup tamamlandı: {Count} kayıt silindi. Retention: {Days} gün. Cutoff: {Cutoff}", deleted, retentionDays, cutoffDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AuditLog cleanup sırasında hata oluştu.");
            }
        }
    }
}
