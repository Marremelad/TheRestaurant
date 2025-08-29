using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TheRestaurant.Data;
using TheRestaurant.Repositories;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Security;
using TheRestaurant.Services;
using TheRestaurant.Services.IServices;

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
        
        builder.Services.AddScoped<IReservationHoldRepository, ReservationHoldRepository>();
        builder.Services.AddScoped<IReservationHoldService, ReservationHoldService>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

        builder.Services.AddHostedService<ReservationCleanupService>();
        
        // Configure JWT settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
        builder.Services.AddSingleton(jwtSettings);
        
        // Add authentication services
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization();

        // Register auth service
        builder.Services.AddScoped<IAuthService, AuthService>();
        
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });

        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}