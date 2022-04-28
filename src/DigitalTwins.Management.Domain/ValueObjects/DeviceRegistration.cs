using CloudEngineering.CodeOps.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Domain.ValueObjects
{
    public sealed class DeviceRegistration : ValueObject
    {
        [Required]
        [JsonPropertyName("deviceIdentifier")]
        public string DeviceIdentifier { get; init; }

        [Required]
        [JsonPropertyName("connectionString")]
        public string ConnectionString { get; init; }

        [JsonConstructor]
        public DeviceRegistration(string deviceIdentifier, string connectionString)
        {
            DeviceIdentifier = deviceIdentifier;
            ConnectionString = connectionString;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DeviceIdentifier;
            yield return ConnectionString;
        }
    }
}