using TheRestaurant.Repositories.IRepositories;

namespace TheRestaurant.Services;

/// <summary>
/// Background service that automatically removes old reservations from the database to maintain data hygiene and GDPR compliance.
/// </summary>
public class ReservationCleanupService(
    IServiceProvider serviceProvider,
    ILogger<ReservationCleanupService> logger
    ) : BackgroundService
{
    
    // Cleanup interval set to 24 hours for regular maintenance cycles.
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(24);
    
    /// <summary>
    /// Main execution loop that runs continuously in the background, performing cleanup every 30 minutes.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Wait 1 minute after startup
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoCleanupWork();
            await Task.Delay(_cleanupInterval, stoppingToken);
        }
    }
    
    /// <summary>
    /// Performs the actual cleanup work by finding and deleting reservations older than 6 months.
    /// </summary>
    private async Task DoCleanupWork()
    {
        try
        {
            // Create a new scope to get fresh repository instance for each cleanup cycle.
            using var scope = serviceProvider.CreateScope();
            var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRepository>();

            // Define cutoff time as 6 months ago to determine which reservations are expired.
            var cutoffTime = DateTime.UtcNow.AddMonths(-6);
            var expiredReservations = await reservationRepository.GetExpiredReservationsAsync(cutoffTime);

            // Only perform deletion if there are expired reservations to avoid unnecessary database operations.
            if (expiredReservations.Count != 0)
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