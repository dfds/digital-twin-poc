using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class RestartDeviceCommand : ICommand<string>
    {
        [JsonPropertyName("hubId")]
        public Guid HubId { get; init; }

        [JsonPropertyName("deviceId")]
        public string DeviceId { get; init; }

        [JsonConstructor]
        public RestartDeviceCommand(Guid hubId, string deviceId)
        {
            HubId = hubId;
            DeviceId = deviceId;
        }
    }
}