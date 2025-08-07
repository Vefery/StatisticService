using System.ComponentModel.DataAnnotations;

namespace Statistic.DTOs
{
    public record StatisticEntryDTO(
        [Required] string _id,
        [Required] string name,
        [Required] DateTime startTime,
        [Required] DateTime endTime,
        [Required] string version
    );
}
