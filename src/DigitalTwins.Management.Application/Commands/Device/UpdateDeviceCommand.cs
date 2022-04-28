using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class UpdateDeviceCommand : ICommand<DeviceRoot>
    {
        [JsonPropertyName("device")]
        public DeviceRoot Device { get; init; }

        [JsonConstructor]
        public UpdateDeviceCommand(DeviceRoot device)
        {
            Device = device;
        }
    }
}