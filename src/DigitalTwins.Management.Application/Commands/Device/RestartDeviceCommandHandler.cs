using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class RestartDeviceCommandHandler : ICommandHandler<RestartDeviceCommand, string>
    {
        private readonly IManagementService _managementService;

        public RestartDeviceCommandHandler(IManagementService managementService)
        {
            _managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
        }

        public async Task<string> Handle(RestartDeviceCommand command, CancellationToken cancellationToken = default)
        {
            return await _managementService.RestartDevice(command.HubId, command.DeviceId, cancellationToken);
        }
    }
}