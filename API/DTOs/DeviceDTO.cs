using System.ComponentModel.DataAnnotations;
using Statistic.Entities;

namespace Statistic.DTOs
{
    public interface IDeviceGeneralData
    {
        string _id { get; }
        string name { get; }
    }
    public record DeviceDTO(
        string _id,
        string name
    ) : IDeviceGeneralData
    {
        public static DeviceDTO ToDTO(string deviceId, string name)
        {
            return new DeviceDTO(deviceId, name);
        }
    }
}
