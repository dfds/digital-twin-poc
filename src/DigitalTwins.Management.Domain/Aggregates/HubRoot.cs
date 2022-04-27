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
        private readonly List<DeviceInfo> _devices;
        private readonly string _name;
        private readonly string _connectionString;

        public IEnumerable<DeviceInfo> Devices => _devices.AsReadOnly();

        public string ConnectionString => _connectionString;

        public string Name => _name;

        public HubRoot(string name, string connectionString)
        {
            _name = name;
            _connectionString = connectionString;
            _devices = new List<DeviceInfo>();

            var evt = new HubCreatedEvent(this);

            AddDomainEvent(evt);
        }

        public void AddDevice(DeviceInfo device)
        {
            _devices.Add(device);

            var evt = new DeviceAddedEvent(Id, device);

            AddDomainEvent(evt);
        }

        public void AddDevice(IEnumerable<DeviceInfo> devices)
        {
            foreach (var device in devices)
            { 
                AddDevice(device);
            }
        }

        public void RemoveDevice(DeviceInfo device)
        {
            _devices.Remove(device);

            var evt = new DeviceRemovedEvent(Id, device);

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