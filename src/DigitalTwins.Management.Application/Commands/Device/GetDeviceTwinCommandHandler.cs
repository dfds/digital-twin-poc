using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Services;
using Microsoft.Azure.Devices;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class GetDeviceTwinCommandHandler : ICommandHandler<GetDeviceTwinCommand, string>
    {
        private readonly IManagementService _managementService;

        public GetDeviceTwinCommandHandler(IManagementService managementService)
        {
            _managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
        }

        public async Task<string> Handle(GetDeviceTwinCommand command, CancellationToken cancellationToken = default)
        {
            var hub = await _managementService.GetHubByIdAsync(command.HubId, cancellationToken);
            var device = hub.Devices.SingleOrDefault(d => d.DeviceId == command.DeviceId);

            if (device == null)
            {
                throw new ApplicationException(string.Format("Device with id: {0} not found in Hub with id: {1}", command.DeviceId, command.HubId));
            }

            using var registryManager = RegistryManager.CreateFromConnectionString(hub.ConnectionString);

            var twin = await registryManager.GetTwinAsync(command.DeviceId, cancellationToken);

            return twin.ToJson();
        }
    }
}