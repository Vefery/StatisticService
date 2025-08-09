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
            RouteGroupBuilder group = app.MapGroup("devices")
                .WithParameterValidation();

            group.MapGet("/", async (ILogger<Program> logger, DatabaseContext dbContext) =>
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
                        statusCode: 500
                    );
                }
            });

            group.MapGet("/{_id}/statistics", async (ILogger<Program> logger, DatabaseContext dbContext, string _id) =>
            {
                try
                {
                    List<ResponseStatisticEntryDTO> devices = await dbContext.Statistics
                        .AsNoTracking()
                        .Where(entry => entry.DeviceId == _id)
                        .Select(entry => ResponseStatisticEntryDTO.ToDTO(entry))
                        .ToListAsync();
                    logger.LogInformation("Получены все записи девайса из базы данных");

                    return Results.Ok(devices);
                }
                catch (Exception e)
                {
                    logger.LogError("Ошибка во время получения записей из базы данных: {ER}", e);
                    return Results.Problem(
                        title: "Server Error",
                        statusCode: 500
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
                        statusCode: 500
                    );
                }
            });

            group.MapDelete("/{_id}/statistics/{entryId}", async (ILogger<Program> logger, DatabaseContext dbContext, int entryId) =>
            {
                try
                {
                    StatisticEntry? entry = await dbContext.Statistics.FindAsync(entryId);
                    if (entry is not null)
                    {
                        logger.LogInformation("Найдена нужная запись для удаления");
                        dbContext.Statistics.Remove(entry);
                        await dbContext.SaveChangesAsync();
                        return Results.NoContent();
                    }
                    else
                        return Results.BadRequest();
                } catch (Exception e)
                {
                    logger.LogError("Ошибка во время удаления записи: {ER}", e);
                    return Results.Problem(
                        title: "Server Error",
                        statusCode: 500
                    );
                }
            });

            return group;
        }
    }
}
