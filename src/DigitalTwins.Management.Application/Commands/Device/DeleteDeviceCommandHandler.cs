using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class DeleteHubCommandHandler : ICommandHandler<DeleteDeviceCommand, bool>
    {
        private readonly IDeviceService _deviceService;

        public DeleteHubCommandHandler(IDeviceService deviceService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }

        public async Task<bool> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken = default)
        {
            return await _deviceService.DeleteDeviceAsync(command.DeviceId, cancellationToken);
        }
    }
}