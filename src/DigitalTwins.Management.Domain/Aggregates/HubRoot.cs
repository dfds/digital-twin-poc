using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Entities;
using DigitalTwins.Management.Domain.Events.Hub;
using DigitalTwins.Management.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalTwins.Management.Domain.Aggregates
{
    public sealed class HubRoot : Entity<Guid>, IAggregateRoot
    {
        private readonly List<DeviceRegistration> _deviceRegistrations;
        private readonly string _name;
        private readonly string _connectionString;

        public IEnumerable<DeviceRegistration> DeviceRegistrations => _deviceRegistrations.AsReadOnly();

        public string ConnectionString => _connectionString;

        public string Name => _name;

        public HubRoot(string name, string connectionString)
        {
            _name = name;
            _connectionString = connectionString;
            _deviceRegistrations = new List<DeviceRegistration>();

            var evt = new HubCreatedEvent(this);

            AddDomainEvent(evt);
        }

        public void AddDeviceRegistration(DeviceRegistration deviceRegistration)
        {
            _deviceRegistrations.Add(deviceRegistration);

            var evt = new HubDeviceRegisteredEvent(Id, deviceRegistration);

            AddDomainEvent(evt);
        }

        public void AddDeviceRegistration(IEnumerable<DeviceRegistration> devices)
        {
            foreach (var device in devices)
            {
                AddDeviceRegistration(device);
            }
        }

        public void RemoveDeviceRegistration(DeviceRegistration deviceRegistration)
        {
            _deviceRegistrations.Remove(deviceRegistration);

            var evt = new HubDeviceUnregisteredEvent(Id, deviceRegistration);

            AddDomainEvent(evt);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            if (string.IsNullOrEmpty(_name))
            {
                result.Add(new ValidationResult("Value cannot be null or empty", new []{ "Name" })) ;
            }

            if (string.IsNullOrEmpty(_connectionString))
            {
                result.Add(new ValidationResult("Value cannot be null or empty", new[] { "ConnectionString" }));
            }

            return result;
        }
    }
}