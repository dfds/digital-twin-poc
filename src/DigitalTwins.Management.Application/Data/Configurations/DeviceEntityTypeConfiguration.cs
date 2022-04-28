using DigitalTwins.Management.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostJanitor.Infrastructure.EntityFramework.Configurations
{
    public class DeviceEntityTypeConfiguration : IEntityTypeConfiguration<DeviceRoot>
    {
        public void Configure(EntityTypeBuilder<DeviceRoot> builder)
        {
            builder.Ignore(v => v.DomainEvents);
            builder.Property(v => v.Id);
            builder.Property(v => v.DeviceIdentifier);
            builder.Property(v => v.ConnectionString);
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => v.DeviceIdentifier).IsUnique();
            builder.ToTable("Devices");
        }
    }
}