using System.ComponentModel.DataAnnotations;
using Statistic.Entities;

namespace Statistic.DTOs
{
    public record StatisticEntryDTO(
        [Required] string _id,
        [Required] string name,
        [Required] DateTime startTime,
        [Required] DateTime endTime,
        [Required] string version
    )
    {
        public StatisticEntry ToEntry()
        {
            return new StatisticEntry
            {
                DeviceId = _id,
                Name = name,
                StartTime = startTime,
                EndTime = endTime,
                Version = version
            };
        }

        public static StatisticEntryDTO ToDTO(StatisticEntry entity)
        {
            return new StatisticEntryDTO(entity.DeviceId, entity.Name, entity.StartTime, entity.EndTime, entity.Version);
        }
    }
}
