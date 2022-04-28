using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using System;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class GetDeviceByIdCommand : ICommand<DeviceRoot>
    {
        [JsonPropertyName("deviceId")]
        public Guid DeviceId { get; init; }

        [JsonConstructor]
        public GetDeviceByIdCommand(Guid deviceId)
        {
            DeviceId = deviceId;
        }
    }
}