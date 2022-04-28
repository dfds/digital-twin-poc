using CloudEngineering.CodeOps.Abstractions.Events;
using DigitalTwins.Management.Domain.ValueObjects;
using System;

namespace DigitalTwins.Management.Domain.Events.Hub
{
    public sealed class HubDeviceRegisteredEvent : IDomainEvent
    {
        public Guid HubId { get; private set; }

        public DeviceRegistration DeviceRegistration { get; private set; }

        public HubDeviceRegisteredEvent(Guid hubId, DeviceRegistration deviceRegistration)
        {
            HubId = hubId;
            DeviceRegistration = deviceRegistration;
        }
    }
}