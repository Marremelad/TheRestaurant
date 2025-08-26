using TheRestaurant.Utilities;

namespace TheRestaurant.Middleware;

public class TableTimeSlotMatrixMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILogger<TableTimeSlotMatrixMiddleware> logger)
    {
        try
        {
            var matrix = context.RequestServices.GetRequiredService<TableTimeSlotMatrix>();
            matrix.PopulateMatrix();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to populate Table + Time slot matrix.");
        }

        await next(context);
    }
}