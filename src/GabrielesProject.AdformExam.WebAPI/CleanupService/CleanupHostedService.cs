using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielesProject.AdformExam.Application.Services
{
    public class CleanupHostedService : BackgroundService
    {
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);
        private readonly ILogger<CleanupHostedService> _logger;

        public CleanupHostedService(ILogger<CleanupHostedService> logger)
        {
            _logger = logger;
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
            _logger.LogInformation("Cleaning up...");
            await Task.Delay(TimeSpan.FromSeconds(1)); 
            _logger.LogInformation("Clean up complete.");
        }
    }
}
