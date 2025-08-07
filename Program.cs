using Statistic.Endpoints;

namespace Statistic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            app.MapStatisticEndpoints();

            app.Run();
        }
    }
}
