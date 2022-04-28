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
    public sealed class GetDeviceByIdCommandHandler : ICommandHandler<GetDeviceByIdCommand, DeviceRoot>, ICommandHandler<GetDeviceByIdCommand, IAggregateRoot>
    {
        private readonly IDeviceService _deviceService;

        public GetDeviceByIdCommandHandler(IDeviceService deviceService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }

        public async Task<DeviceRoot> Handle(GetDeviceByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _deviceService.GetDeviceByIdAsync(command.DeviceId, cancellationToken);
        }

        async Task<IAggregateRoot> IRequestHandler<GetDeviceByIdCommand, IAggregateRoot>.Handle(GetDeviceByIdCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> ICommandHandler<GetDeviceByIdCommand, IAggregateRoot>.Handle(GetDeviceByIdCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}