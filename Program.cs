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

            var app = builder.Build();

            app.MapStatisticEndpoints();

            app.Run();
        }
    }
}
