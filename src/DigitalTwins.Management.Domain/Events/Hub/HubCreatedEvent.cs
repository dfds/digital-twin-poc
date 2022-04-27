using CloudEngineering.CodeOps.Abstractions.Events;
using DigitalTwins.Management.Domain.Aggregates;

namespace DigitalTwins.Management.Domain.Events.Hub
{
    public sealed class HubCreatedEvent : IDomainEvent
    {
        public HubRoot Hub { get; private set; }

        public HubCreatedEvent(HubRoot hub)
        {
            Hub = hub;
        }
    }
}