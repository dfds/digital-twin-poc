using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Entities;
using DigitalTwins.Management.Domain.Events.Hub;
using DigitalTwins.Management.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalTwins.Management.Domain.Aggregates
{
    public sealed class DeviceRoot : Entity<Guid>, IAggregateRoot
    {
        private readonly string _deviceIdentifier;
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public string DeviceIdentifier => _deviceIdentifier;

        public DeviceRoot(string deviceIdentifier, string connectionString)
        {
            _deviceIdentifier = deviceIdentifier;
            _connectionString = connectionString;

            var evt = new DeviceCreatedEvent(this);

            AddDomainEvent(evt);
        }

        public DeviceRegistration GetDeviceRegistration()
        {
            return new DeviceRegistration(_deviceIdentifier, _connectionString);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            if (string.IsNullOrEmpty(_deviceIdentifier))
            {
                result.Add(new ValidationResult("Value cannot be null or empty", new []{ "DeviceIdentifier" })) ;
            }

            if (string.IsNullOrEmpty(_connectionString))
            {
                result.Add(new ValidationResult("Value cannot be null or empty", new[] { "ConnectionString" }));
            }

            return result;
        }
    }
}