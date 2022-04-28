using CloudEngineering.CodeOps.Abstractions.Events;
using DigitalTwins.Management.Domain.Aggregates;

namespace DigitalTwins.Management.Domain.Events.Hub
{
    public sealed class DeviceCreatedEvent : IDomainEvent
    {
        public DeviceRoot Device { get; private set; }

        public DeviceCreatedEvent(DeviceRoot device)
        {
            Device = device;
        }
    }
}