﻿// <auto-generated />
using System;
using DigitalTwins.Management.Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DigitalTwins.Management.Domain.Aggregates.DeviceRoot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConnectionString")
                        .HasColumnType("text");

                    b.Property<string>("DeviceIdentifier")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DeviceIdentifier")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("DigitalTwins.Management.Domain.Aggregates.HubRoot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConnectionString")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Hubs");
                });

            modelBuilder.Entity("DigitalTwins.Management.Domain.Aggregates.HubRoot", b =>
                {
                    b.OwnsMany("DigitalTwins.Management.Domain.ValueObjects.DeviceRegistration", "DeviceRegistrations", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("ConnectionString")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("DeviceIdentifier")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("DeviceIdentifier")
                                .IsUnique();

                            b1.HasIndex("OwnerId");

                            b1.ToTable("DeviceRegistration");

                            b1.WithOwner()
                                .HasForeignKey("OwnerId");
                        });

                    b.Navigation("DeviceRegistrations");
                });
#pragma warning restore 612, 618
        }
    }
}
