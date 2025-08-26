using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Middleware;
using TheRestaurant.Repositories;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<TheRestaurantDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddScoped<ITableRepository, TableRepository>();
        builder.Services.AddScoped<ITableService, TableService>();

        builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
        builder.Services.AddScoped<IReservationService, ReservationService>();

        builder.Services.AddScoped<DbSetTracker>();
        builder.Services.AddScoped<TableTimeSlotMatrix>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<DbSetTrackerMiddleware>();
        app.UseMiddleware<TableTimeSlotMatrixMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}