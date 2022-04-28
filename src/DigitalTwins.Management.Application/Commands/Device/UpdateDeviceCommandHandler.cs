using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Device
{
    public sealed class UpdateDeviceCommandHandler : ICommandHandler<UpdateDeviceCommand, DeviceRoot>, ICommandHandler<UpdateDeviceCommand, IAggregateRoot>
    {
        private readonly IDeviceService _deviceService;

        public UpdateDeviceCommandHandler(IDeviceService deviceService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }

        public async Task<DeviceRoot> Handle(UpdateDeviceCommand command, CancellationToken cancellationToken = default)
        {
            return await _deviceService.UpdateDeviceAsync(command.Device, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<UpdateDeviceCommand, IAggregateRoot>.Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> ICommandHandler<UpdateDeviceCommand, IAggregateRoot>.Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}