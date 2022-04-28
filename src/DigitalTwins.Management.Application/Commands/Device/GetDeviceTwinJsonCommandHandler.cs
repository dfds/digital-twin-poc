using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class GetDeviceTwinJsonCommandHandler : ICommandHandler<GetDeviceTwinJsonCommand, string>
    {
        private readonly IHubService _hubService;

        public GetDeviceTwinJsonCommandHandler(IHubService hubService)
        {
            _hubService = hubService ?? throw new ArgumentNullException(nameof(hubService));
        }

        public async Task<string> Handle(GetDeviceTwinJsonCommand command, CancellationToken cancellationToken = default)
        {
            return await _hubService.GetDeviceTwinJsonAsync(command.DeviceIdentifier, cancellationToken);
        }
    }
}