using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class DeleteHubCommand : ICommand<bool>
    {
        [JsonPropertyName("hubId")]
        public Guid HubId { get; init; }

        [JsonConstructor]
        public DeleteHubCommand(Guid hubId)
        {
            HubId = hubId;
        }
    }
}