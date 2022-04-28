using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.ValueObjects;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class CreateHubCommand : ICommand<HubRoot>
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("connectionString")]
        public string ConnectionString { get; init; }

        [JsonPropertyName("devices")]
        public IEnumerable<DeviceRegistration> Devices { get; init; }

        [JsonConstructor]
        public CreateHubCommand(string name, string connectionString, IEnumerable<DeviceRegistration> devices = default)
        {
            Name = name;
            ConnectionString = connectionString;
            Devices = devices;
        }
    }
}