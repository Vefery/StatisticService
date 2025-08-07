namespace Statistic.Entities
{
    public class StatisticEntry
    {
        public int Id { get; set; }
        public required string DeviceId { get; set; }
        public required string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Version { get; set; }
    }
}
