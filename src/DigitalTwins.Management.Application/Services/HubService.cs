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
    public class HubService : IHubService
    {
        private readonly IHubRepository _hubRepository;

        public HubService(IHubRepository hubRepository)
        {
            _hubRepository = hubRepository;
        }

        public async Task<HubRoot> GetHubByIdAsync(Guid hubId, CancellationToken cancellationToken = default)
        {
            var hubRoot = await _hubRepository.GetAsync(h => h.Id == hubId, cancellationToken);

            return hubRoot.SingleOrDefault();
        }

        public async Task<HubRoot> AddHubAsync(string name, string connectionString, IEnumerable<DeviceRegistration> devices = null, CancellationToken cancellationToken = default)
        {
            var hubRoot = new HubRoot(name, connectionString);

            hubRoot.AddDeviceRegistration(devices);

            hubRoot = _hubRepository.Add(hubRoot);

            await _hubRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return hubRoot;
        }

        public async Task<HubRoot> UpdateHubAsync(HubRoot hub, CancellationToken cancellationToken = default)
        {
            var hubRoot = _hubRepository.Update(hub);

            await _hubRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return hubRoot;
        }

        public async Task<bool> DeleteHubAsync(Guid hubId, CancellationToken cancellationToken = default)
        {
            var hubRoot = await _hubRepository.GetAsync(hubId, cancellationToken);

            _hubRepository.Delete(hubRoot);

            await _hubRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }

        public async Task<string> InvokeDeviceMethodAsync(string deviceIdentifier, DeviceMethod method, TimeSpan? connectionTimeout = default, TimeSpan? responseTimeout = default, CancellationToken cancellationToken = default)
        {
            var hub = await GetHubByDeviceIdentifierAsync(deviceIdentifier);

            using var serviceClient = hub.GetServiceClient();
            var deviceMethod = new CloudToDeviceMethod(method.Name);

            if (connectionTimeout.HasValue)
                deviceMethod.ConnectionTimeout = connectionTimeout.Value;

            if (responseTimeout.HasValue)
                deviceMethod.ResponseTimeout = responseTimeout.Value;
            else
                deviceMethod.ResponseTimeout = TimeSpan.FromSeconds(30);

            var result = await serviceClient.InvokeDeviceMethodAsync(deviceIdentifier, deviceMethod, cancellationToken);

            return result.GetPayloadAsJson();
        }

        public async Task<string> GetDeviceTwinJsonAsync(string deviceIdentifier, CancellationToken cancellationToken = default)
        {
            var hub = await GetHubByDeviceIdentifierAsync(deviceIdentifier, cancellationToken);

            using var registryManager = RegistryManager.CreateFromConnectionString(hub.ConnectionString);

            var twin = await registryManager.GetTwinAsync(deviceIdentifier, cancellationToken);

            return twin.ToJson();
        }

        private async Task<HubRoot> GetHubByDeviceIdentifierAsync(string deviceIdentifier, CancellationToken cancellationToken = default)
        {
            var hub = (await _hubRepository.GetAsync((hub) => hub.DeviceRegistrations.Any(dr => dr.DeviceIdentifier == deviceIdentifier), cancellationToken)).FirstOrDefault();

            if (hub == null)
            {
                throw new ApplicationException(string.Format("Device with id: {0} not found in any of the known hubs", deviceIdentifier));
            }

            return hub;
        }
    }
}