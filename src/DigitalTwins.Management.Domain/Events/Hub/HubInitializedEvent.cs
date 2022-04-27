using CloudEngineering.CodeOps.Abstractions.Events;
using DigitalTwins.Management.Domain.Aggregates;

namespace DigitalTwins.Management.Domain.Events.Hub
{
    public sealed class HubInitializedEvent : IDomainEvent
    {
        public HubRoot Hub { get; private set; }

        public HubInitializedEvent(HubRoot hub)
        {
            Hub = hub;
        }
    }
}