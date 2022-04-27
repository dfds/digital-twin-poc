using CloudEngineering.CodeOps.Abstractions.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DigitalTwins.Management.Domain.ValueObjects
{
    public sealed class DeviceInfo : ValueObject
    {
        [Required]
        [JsonPropertyName("deviceId")]
        public string DeviceId { get; init; }

        [Required]
        [JsonPropertyName("connectionString")]
        public string ConnectionString { get; init; }

        [JsonConstructor]
        public DeviceInfo(string deviceId, string connectionString)
        {
            DeviceId = deviceId;
            ConnectionString = connectionString;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DeviceId;
            yield return ConnectionString;
        }
    }
}