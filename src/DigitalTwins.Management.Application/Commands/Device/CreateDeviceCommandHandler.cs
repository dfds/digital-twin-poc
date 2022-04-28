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
    public sealed class CreateDeviceCommandHandler : ICommandHandler<CreateDeviceCommand, DeviceRoot>, ICommandHandler<CreateDeviceCommand, IAggregateRoot>
    {
        private readonly IDeviceService _deviceService;

        public CreateDeviceCommandHandler(IDeviceService deviceService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }

        public async Task<DeviceRoot> Handle(CreateDeviceCommand command, CancellationToken cancellationToken = default)
        {
            return await _deviceService.AddDeviceAsync(command.DeviceIdentifier, command.ConnectionString, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<CreateDeviceCommand, IAggregateRoot>.Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> ICommandHandler<CreateDeviceCommand, IAggregateRoot>.Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}