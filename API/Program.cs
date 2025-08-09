using Microsoft.EntityFrameworkCore;
using Statistic.Data;
using Statistic.Endpoints;

namespace Statistic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? ["http://localhost:4200"];

            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DbName") ?? "TestDatabase")
            );
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Logging.ClearProviders();
            builder.Logging.AddDebug();

            var app = builder.Build();
    
            app.UseCors();
            app.MapStatisticEndpoints();
            app.Logger.LogInformation("Added endpoints");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
