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
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DbName") ?? "TestDatabase")
            );
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            builder.Logging.ClearProviders();
            builder.Logging.AddDebug();

            var app = builder.Build();

            app.UseCors();
            app.MapStatisticEndpoints();
            app.Logger.LogInformation("Added endpoints");

            app.Run();
        }
    }
}
