using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using System;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class GetHubByIdCommand : ICommand<HubRoot>
    {
        [JsonPropertyName("hubId")]
        public Guid HubId { get; init; }

        [JsonConstructor]
        public GetHubByIdCommand(Guid hubId)
        {
            HubId = hubId;
        }
    }
}