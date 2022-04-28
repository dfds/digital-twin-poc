using CloudEngineering.CodeOps.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class GetDeviceTwinJsonCommand : ICommand<string>
    {
        [JsonPropertyName("deviceIdentifier")]
        public string DeviceIdentifier { get; init; }

        [JsonConstructor]
        public GetDeviceTwinJsonCommand(string deviceIdentifier)
        {
            DeviceIdentifier = deviceIdentifier;
        }
    }
}