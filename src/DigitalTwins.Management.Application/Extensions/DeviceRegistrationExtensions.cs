using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.ValueObjects;
using Microsoft.Azure.Devices.Client;

namespace DigitalTwins.Management.Application.Extensions
{
    internal static class DeviceExtensions
    {
        internal static DeviceClient GetDeviceClient(this DeviceRegistration deviceRegistration, TransportType transportType = TransportType.Mqtt) 
        { 
           return GetDeviceClient(deviceRegistration.ConnectionString, transportType);
        }

        internal static DeviceClient GetDeviceClient(this DeviceRoot device, TransportType transportType = TransportType.Mqtt)
        {
            return GetDeviceClient(device.ConnectionString, transportType);
        }

        private static DeviceClient GetDeviceClient(string connectionString, TransportType transportType = TransportType.Mqtt)
        {
            return DeviceClient.CreateFromConnectionString(connectionString, transportType);
        }
    }
}
