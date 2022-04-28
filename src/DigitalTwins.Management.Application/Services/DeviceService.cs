using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.Repositories;
using DigitalTwins.Management.Domain.Services;
using Microsoft.Azure.Devices.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public DeviceService()
        {
        }

        public async Task<DeviceRoot> GetDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
        {
            var deviceRoot = await _deviceRepository.GetAsync(d => d.Id == deviceId, cancellationToken);

            return deviceRoot.SingleOrDefault();
        }

        public async Task<DeviceRoot> AddDeviceAsync(string deviceIdentifier, string connectionString, CancellationToken cancellationToken = default)
        {
            var deviceRoot = new DeviceRoot(deviceIdentifier, connectionString);

            deviceRoot = _deviceRepository.Add(deviceRoot);

            await _deviceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return deviceRoot;
        }

        public async Task<DeviceRoot> UpdateDeviceAsync(DeviceRoot device, CancellationToken cancellationToken = default)
        {
            var deviceRoot = _deviceRepository.Update(device);

            await _deviceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return deviceRoot;
        }

        public async Task<bool> DeleteDeviceAsync(Guid deviceId, CancellationToken cancellationToken = default)
        {
            var deviceRoot = await _deviceRepository.GetAsync(deviceId, cancellationToken);

            _deviceRepository.Delete(deviceRoot);

            await _deviceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }

        public async Task RegisterDeviceCallbackAsync(string deviceIdentifier, DeviceMethod method, Action callback = null, CancellationToken cancellationToken = default)
        {
            var device = (await _deviceRepository.GetAsync(d => d.DeviceIdentifier == deviceIdentifier, cancellationToken)).SingleOrDefault();

            if (device == null)
            {
                throw new ApplicationException(string.Format("Device with identifier: {0} could not be found", deviceIdentifier));
            }

            var deviceClient = DeviceClient.CreateFromConnectionString(device.ConnectionString);

            Task<MethodResponse> callbackHandler(MethodRequest request, object userContext)
            {
                //TODO: Verify that delegate is called
                callback();

                var result = "{\"result\":\"" + method.Name + " started.\"}";

                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
            }

            await deviceClient.SetMethodHandlerAsync(method.Name, callbackHandler, null, cancellationToken);

            return;
        }
    }
}
