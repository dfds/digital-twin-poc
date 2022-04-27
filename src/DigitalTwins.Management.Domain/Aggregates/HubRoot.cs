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
        private readonly List<Device> _devices;
        private readonly string _connectionString;

        public IEnumerable<Device> Devices => _devices.AsReadOnly();

        public string ConnectionString => _connectionString;

        public HubRoot(string connectionString)
        {
            _connectionString = connectionString;
            _devices = new List<Device>();

            var evt = new HubInitializedEvent(this);

            AddDomainEvent(evt);
        }

        public void AddDevice(Device device)
        {
            _devices.Add(device);

            var evt = new DeviceAddedEvent(Id, device);

            AddDomainEvent(evt);
        }

        public void AddDevice(IEnumerable<Device> devices)
        {
            foreach (var device in devices)
            { 
                AddDevice(device);
            }
        }

        public void RemoveDevice(Device device)
        {
            _devices.Remove(device);

            var evt = new DeviceRemovedEvent(Id, device);

            AddDomainEvent(evt);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            if (string.IsNullOrEmpty(_connectionString))
            {
                result.Add(new ValidationResult("ConnectionString is empty", new []{ "ConnectionString" })) ;
            }

            return result;
        }
    }
}