using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class DeleteDeviceCommand : ICommand<bool>
    {
        [JsonPropertyName("deviceId")]
        public Guid DeviceId { get; init; }

        [JsonConstructor]
        public DeleteDeviceCommand(Guid deviceId)
        {
            DeviceId = deviceId;
        }
    }
}