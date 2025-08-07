using System.Runtime.CompilerServices;

namespace Statistic.Endpoints
{
    public static class StatisticsEndpoints
    {
        public static RouteGroupBuilder MapStatisticEndpoints(this WebApplication app)
        {
            RouteGroupBuilder group = app.MapGroup("statistics");

            group.MapGet("/", () =>
            {
                return Results.NoContent;
            });

            return group;
        }
    }
}
