using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class SendDeviceCommandHandler : ICommandHandler<SendDeviceCommand, string>
    {
        private readonly IHubService _hubService;

        public SendDeviceCommandHandler(IHubService hubService)
        {
            _hubService = hubService ?? throw new ArgumentNullException(nameof(hubService));
        }

        public async Task<string> Handle(SendDeviceCommand command, CancellationToken cancellationToken = default)
        {
            return await _hubService.InvokeDeviceMethodAsync(command.DeviceIdentifier, command.DeviceMethod, command.ConnectionTimeout, command.ResponseTimeout, cancellationToken);
        }
    }
}