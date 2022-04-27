using CloudEngineering.CodeOps.Abstractions.Events;
using DigitalTwins.Management.Domain.ValueObjects;
using System;

namespace DigitalTwins.Management.Domain.Events.Hub
{
    public sealed class DeviceAddedEvent : IDomainEvent
    {
        public Guid HubId { get; private set; }

        public DeviceInfo Device { get; private set; }

        public DeviceAddedEvent(Guid hubId, DeviceInfo device)
        {
            HubId = hubId;
            Device = device;
        }
    }
}