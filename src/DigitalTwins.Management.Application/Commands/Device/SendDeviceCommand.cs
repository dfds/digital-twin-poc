using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using System;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class SendDeviceCommand : ICommand<string>
    {
        [JsonPropertyName("deviceIdentifier")]
        public string DeviceIdentifier { get; init; }

        [JsonPropertyName("deviceMethod")]
        public DeviceMethod DeviceMethod { get; init; }

        [JsonPropertyName("connectionTimeout")]
        public TimeSpan? ConnectionTimeout { get; init; }

        [JsonPropertyName("responseTimeout")]
        public TimeSpan? ResponseTimeout { get; init; }

        [JsonConstructor]
        public SendDeviceCommand(string deviceIdentifier, DeviceMethod deviceMethod)
        {
            DeviceIdentifier = deviceIdentifier;
            DeviceMethod = deviceMethod;
        }
    }
}