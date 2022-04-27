using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class UpdateHubCommand : ICommand<HubRoot>
    {
        [JsonPropertyName("hub")]
        public HubRoot Hub { get; init; }

        [JsonConstructor]
        public UpdateHubCommand(HubRoot hub)
        {
            Hub = hub;
        }
    }
}