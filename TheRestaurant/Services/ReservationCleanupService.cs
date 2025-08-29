using TheRestaurant.Repositories.IRepositories;

namespace TheRestaurant.Services;

public class ReservationCleanupService(
    IServiceProvider serviceProvider,
    ILogger<ReservationCleanupService> logger
    ) : BackgroundService
{
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromMinutes(30);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoCleanupWork(stoppingToken);
            await Task.Delay(_cleanupInterval, stoppingToken);
        }
    }
    
    private async Task DoCleanupWork(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRepository>();

            var cutoffTime = DateTime.UtcNow.AddMonths(-6);
            var expiredReservations = await reservationRepository.GetExpiredReservationsAsync(cutoffTime);

            if (expiredReservations.Any())
            {
                await reservationRepository.DeleteReservationsAsync(expiredReservations);
                logger.LogInformation("Deleted {Count} expired reservations", expiredReservations.Count);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred during reservation cleanup");
        }
    }
}