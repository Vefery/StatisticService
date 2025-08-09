using System.ComponentModel.DataAnnotations;
using Statistic.Entities;

namespace Statistic.DTOs
{
    public record ResponseStatisticEntryDTO(
        int id,
        string _id,
        string name,
        DateTime startTime,
        DateTime endTime,
        string version
    ) : IDeviceGeneralData
    {
        public static ResponseStatisticEntryDTO ToDTO(StatisticEntry entity)
        {
            return new ResponseStatisticEntryDTO(entity.Id, entity.DeviceId, entity.Name, entity.StartTime, entity.EndTime, entity.Version);
        }
    }
}
