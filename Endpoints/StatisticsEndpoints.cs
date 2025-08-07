using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Statistic.Data;
using Statistic.DTOs;
using Statistic.Entities;

namespace Statistic.Endpoints
{
    public static class StatisticsEndpoints
    {
        const string getStatisticsName = "GetEntry";
        public static RouteGroupBuilder MapStatisticEndpoints(this WebApplication app)
        {
            RouteGroupBuilder group = app.MapGroup("statistics")
                .WithParameterValidation();

            group.MapGet("/", async (ILogger<Program> logger, DatabaseContext dbContext) =>
            {
                try
                {
                    List<StatisticEntryDTO> statistics = await dbContext.Statistics
                        .AsNoTracking()
                        .Select(entry => StatisticEntryDTO.ToDTO(entry))
                        .ToListAsync();
                    logger.LogInformation("Получены все записи из базы данных");

                    StatisticsDTO staitisticsListDto = new (statistics);
                    logger.LogInformation("Создано DTO со списком записей");

                    return Results.Ok(staitisticsListDto);
                }
                catch (Exception e)
                {
                    logger.LogError("Ошибка во время получения записей из базы данных: {ER}", e);
                    return Results.Problem(
                        title: "Server Error",
                        statusCode: 500,
                        detail: "Ошибка обращения к базе данных"
                    );
                }
            })
                .WithName(getStatisticsName);

            group.MapPost("/", async (ILogger<Program> logger, DatabaseContext dbContext, StatisticEntryDTO entryDto) =>
            {
                try
                {
                    await dbContext.AddAsync(entryDto.ToEntry());
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Добавлена запись");

                    return Results.CreatedAtRoute(getStatisticsName, new
                    {
                        entryDto._id,
                        entryDto.name,
                        entryDto.startTime,
                        entryDto.endTime,
                        entryDto.version
                    }, entryDto);
                } 
                catch (Exception e)
                {
                    logger.LogError("Ошибка во время добавления записи: {ER}", e);
                    return Results.Problem(
                        title: "Server Error",
                        statusCode: 500,
                        detail: "Ошибка обращения к базе данных"
                    );
                }
            });

            return group;
        }
    }
}
