using TheRestaurant.Utilities;

namespace TheRestaurant.Middleware;

public class DbSetTrackerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILogger<DbSetTrackerMiddleware> logger)
    {
        try
        {
            var tracker = context.RequestServices.GetRequiredService<DbSetTracker>();
            await tracker.LoadDataAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load data for DbSetTracker.");
        }
        
        await next(context);
    }
}