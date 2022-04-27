using CloudEngineering.CodeOps.Abstractions.Data;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DigitalTwins.Management.Application.Data
{
    public class ApplicationContext : EntityContext
    {
        public virtual DbSet<Device> Devices { get; set; }

        public virtual DbSet<HubRoot> Hubs { get; set; }

        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions options, IMediator mediator = default, IDictionary<Type, IEnumerable<IView>> seedData = default) : base(options)
        {

        }
    }
}
