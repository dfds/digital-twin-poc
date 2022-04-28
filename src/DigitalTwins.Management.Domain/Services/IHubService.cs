using CloudEngineering.CodeOps.Abstractions.Services;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Domain.Services
{
    public interface IHubService : IService
    {
        Task<HubRoot> GetHubByIdAsync(Guid hubId, CancellationToken cancellationToken = default);

        Task<HubRoot> AddHubAsync(string name, string connectionString, IEnumerable<DeviceRegistration> devices = default, CancellationToken cancellationToken = default);

        Task<HubRoot> UpdateHubAsync(HubRoot hub, CancellationToken cancellationToken = default);

        Task<bool> DeleteHubAsync(Guid hubId, CancellationToken cancellationToken = default);

        Task<string> InvokeDeviceMethodAsync(string deviceIdentifier, DeviceMethod method, TimeSpan? connectionTimeout = default, TimeSpan? responseTimeout = default, CancellationToken cancellationToken = default);

        Task<string> GetDeviceTwinJsonAsync(string deviceIdentifier, CancellationToken cancellationToken = default);
    }
}