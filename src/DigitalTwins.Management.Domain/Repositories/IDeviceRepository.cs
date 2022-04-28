using CloudEngineering.CodeOps.Abstractions.Repositories;
using DigitalTwins.Management.Domain.Aggregates;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Domain.Repositories
{
    public interface IDeviceRepository : IRepository<DeviceRoot>
    {
        Task<DeviceRoot> GetAsync(Guid deviceId, CancellationToken cancellationToken = default);
    }
}