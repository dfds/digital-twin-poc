using CloudEngineering.CodeOps.Abstractions.Repositories;
using DigitalTwins.Management.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Domain.Repositories
{
    public interface IHubRepository : IRepository<HubRoot>
    {
        Task<HubRoot> GetAsync(Guid hubId);
    }
}