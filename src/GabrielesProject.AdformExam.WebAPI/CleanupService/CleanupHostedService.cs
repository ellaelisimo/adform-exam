using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Exceptions;

namespace GabrielesProject.AdformExam.Application.Services
{
    public class CleanupHostedService : BackgroundService
    {
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(20);

        private readonly ILogger<CleanupHostedService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CleanupHostedService(ILogger<CleanupHostedService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("CleanupService is running.");
                await Cleanup();

                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task Cleanup()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var ordersService = scope.ServiceProvider.GetRequiredService<IOrdersService>();

            try
            {
                await ordersService.DeleteAsNotPaidAfterTwoHours();
            }
            catch (CleanupException ex)
            {
                _logger.LogError(ex, "Error occurred during cleanup.");
            }
        }
    }
}
