using CloudEngineering.CodeOps.Abstractions.Entities;

namespace DigitalTwins.Management.Domain.Aggregates
{
    //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
    public class DeviceMethod : EntityEnumeration
    {
        public static DeviceMethod Reboot = new(1, nameof(Reboot));
        public static DeviceMethod SetTelemetryInterval = new(2, nameof(SetTelemetryInterval));

        public DeviceMethod(int id, string name)
            : base(id, name)
        {
        }
    }
}
