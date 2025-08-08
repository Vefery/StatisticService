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

            group.MapGet("/devices", async (ILogger<Program> logger, DatabaseContext dbContext) =>
            {
                try
                {
                    List<DeviceDTO> devices = await dbContext.Statistics
                        .AsNoTracking()
                        .GroupBy(entry => new { entry.DeviceId, entry.Name })
                        .Select(entry => DeviceDTO.ToDTO(entry.Key.DeviceId, entry.Key.Name, entry.Count()))
                        .ToListAsync();
                    logger.LogInformation("Получены все девавйсы из базы данных");

                    return Results.Ok(devices);
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
            });

            group.MapGet("/devices/{_id}", async (ILogger<Program> logger, DatabaseContext dbContext, string _id) =>
            {
                try
                {
                    List<StatisticEntryDTO> devices = await dbContext.Statistics
                        .AsNoTracking()
                        .Where(entry => entry.DeviceId == _id)
                        .Select(entry => StatisticEntryDTO.ToDTO(entry))
                        .ToListAsync();
                    logger.LogInformation("Получены все записи девайса из базы данных");

                    return Results.Ok(devices);
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
