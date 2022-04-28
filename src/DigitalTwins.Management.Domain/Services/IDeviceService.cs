using CloudEngineering.CodeOps.Abstractions.Services;
using DigitalTwins.Management.Domain.Aggregates;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Domain.Services
{
    public interface IDeviceService : IService
    {
        Task<DeviceRoot> GetDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken = default);

        Task<DeviceRoot> AddDeviceAsync(string deviceIdentifier, string connectionString, CancellationToken cancellationToken = default);

        Task<DeviceRoot> UpdateDeviceAsync(DeviceRoot device, CancellationToken cancellationToken = default);

        Task<bool> DeleteDeviceAsync(Guid deviceId, CancellationToken cancellationToken = default);

        Task RegisterDeviceCallbackAsync(string deviceIdentifier, DeviceMethod method, Action callback = default, CancellationToken cancellationToken = default);
    }
}
