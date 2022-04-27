using DigitalTwins.Management.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CostJanitor.Infrastructure.EntityFramework.Configurations
{
    public class HubEntityTypeConfiguration : IEntityTypeConfiguration<HubRoot>
    {
        public void Configure(EntityTypeBuilder<HubRoot> builder)
        {
            builder.Ignore(v => v.DomainEvents);
            builder.Property(v => v.Id).IsRequired();
            builder.Property(v => v.Name);
            builder.Property(v => v.ConnectionString);
            builder.HasKey(v => v.Id);
            builder.ToTable("Hubs");

            builder.OwnsMany(
            p => p.Devices, a =>
            {
                a.WithOwner().HasForeignKey("OwnerId");
                a.Property<Guid>("Id");
                a.HasKey("Id");
            });
        }
    }
}