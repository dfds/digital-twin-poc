using DigitalTwins.Management.Domain.ValueObjects;
using Microsoft.Azure.Devices.Client;

namespace DigitalTwins.Management.Application.Extensions
{
    internal static class DeviceInfoExtensions
    {
        internal static DeviceClient GetDeviceClient(this DeviceInfo deviceInfo) 
        { 
           return DeviceClient.CreateFromConnectionString(deviceInfo.ConnectionString, TransportType.Mqtt);
        }
    }
}
