using System.Runtime.CompilerServices;
using Statistic.Data;

namespace Statistic.Endpoints
{
    public static class StatisticsEndpoints
    {
        public static RouteGroupBuilder MapStatisticEndpoints(this WebApplication app)
        {
            RouteGroupBuilder group = app.MapGroup("statistics")
                .WithParameterValidation();

            group.MapGet("/", (ILogger<Program> logger, DatabaseContext dbContext) =>
            {

                return Results.Ok();
            });

            group.MapPost("/", (ILogger<Program> logger, DatabaseContext dbContext) =>
            {

                return Results.Ok();
            });

            return group;
        }
    }
}
