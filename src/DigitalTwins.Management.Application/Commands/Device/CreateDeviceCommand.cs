using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class CreateDeviceCommand : ICommand<DeviceRoot>
    {
        [JsonPropertyName("deviceIdentifier")]
        public string DeviceIdentifier { get; init; }

        [JsonPropertyName("connectionString")]
        public string ConnectionString { get; init; }

        [JsonConstructor]
        public CreateDeviceCommand(string deviceIdentifier, string connectionString)
        {
            DeviceIdentifier = deviceIdentifier;
            ConnectionString = connectionString;
        }
    }
}