using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class RestartDeviceCommand : ICommand<string>
    {
        [JsonPropertyName("hub")]
        public HubRoot Hub { get; init; }

        [JsonPropertyName("deviceInfo")]
        public DeviceInfo Device { get; init; }

        [JsonConstructor]
        public RestartDeviceCommand(HubRoot hub, DeviceInfo device)
        {
            Hub = hub;
            Device = device;
        }
    }
}