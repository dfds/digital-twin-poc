using DigitalTwins.Management.Application.Extensions;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.Repositories;
using DigitalTwins.Management.Domain.Services;
using DigitalTwins.Management.Domain.ValueObjects;
using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IHubRepository _hubRepository;

        public ManagementService(IHubRepository hubRepository)
        {
            _hubRepository = hubRepository;
        }

        public async Task<HubRoot> GetHubById(Guid hubId, CancellationToken ct = default)
        {
            var hubRoot = await _hubRepository.GetAsync(h => h.Id == hubId);

            return hubRoot.SingleOrDefault();
        }

        public async Task<HubRoot> AddHubAsync(string name, string connectionString, IEnumerable<DeviceInfo> devices = null, CancellationToken ct = default)
        {
            var hubRoot = new HubRoot(name, connectionString);

            hubRoot.AddDevice(devices);

            hubRoot = _hubRepository.Add(hubRoot);

            await _hubRepository.UnitOfWork.SaveEntitiesAsync(ct);

            return hubRoot;
        }

        public async Task<HubRoot> UpdateHubAsync(HubRoot hub, CancellationToken ct = default)
        {
            var hubRoot = _hubRepository.Update(hub);

            await _hubRepository.UnitOfWork.SaveEntitiesAsync(ct);

            return hubRoot;
        }

        public async Task<bool> DeleteHubAsync(Guid hubId, CancellationToken ct = default)
        {
            var hubRoot = await _hubRepository.GetAsync(hubId);

            _hubRepository.Delete(hubRoot);

            await _hubRepository.UnitOfWork.SaveEntitiesAsync(ct);

            return true;
        }

        public async Task<string> RestartDevice(Guid hubId, string deviceId, CancellationToken ct = default)
        {
            var hub = await _hubRepository.GetAsync(hubId, ct);
            var device = hub.Devices.SingleOrDefault(d => d.DeviceId == deviceId);

            if (device == null)
            {
                throw new ApplicationException(string.Format("Device with id: {0} not found in Hub with id: {1}", deviceId, hubId));
            }

            using var serviceClient = hub.GetServiceClient();
            var method = new CloudToDeviceMethod("reboot")
            {
                ResponseTimeout = TimeSpan.FromSeconds(30)
            };

            var result = await serviceClient.InvokeDeviceMethodAsync(deviceId, method, ct);

            return result.GetPayloadAsJson();
        }
    }
}