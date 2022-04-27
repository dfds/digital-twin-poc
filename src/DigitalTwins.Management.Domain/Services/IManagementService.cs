using CloudEngineering.CodeOps.Abstractions.Services;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Domain.Services
{
    public interface IManagementService : IService
    {
        Task<HubRoot> GetHubById(Guid hubId, CancellationToken ct = default);

        Task<HubRoot> AddHubAsync(string name, string connectionString, IEnumerable<DeviceInfo> devices = default, CancellationToken ct = default);

        Task<HubRoot> UpdateHubAsync(HubRoot hub, CancellationToken ct = default);

        Task<bool> DeleteHubAsync(Guid hubId, CancellationToken ct = default);

        Task<string> RestartDevice(Guid hubId, string deviceId, CancellationToken ct = default);
    }
}