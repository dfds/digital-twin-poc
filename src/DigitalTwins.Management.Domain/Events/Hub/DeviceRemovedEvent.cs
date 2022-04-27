using CloudEngineering.CodeOps.Abstractions.Events;
using DigitalTwins.Management.Domain.ValueObjects;
using System;

namespace DigitalTwins.Management.Domain.Events.Hub
{
    public sealed class DeviceRemovedEvent : IDomainEvent
    {
        public Guid HubId { get; private set; }

        public Device Device { get; private set; }

        public DeviceRemovedEvent(Guid hubId, Device device)
        {
            HubId = hubId;
            Device = device;
        }
    }
}