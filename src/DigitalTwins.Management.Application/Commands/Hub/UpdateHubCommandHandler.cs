using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class UpdateHubCommandHandler : ICommandHandler<UpdateHubCommand, HubRoot>, ICommandHandler<UpdateHubCommand, IAggregateRoot>
    {
        private readonly IManagementService _managementService;

        public UpdateHubCommandHandler(IManagementService managementService)
        {
            _managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
        }

        public async Task<HubRoot> Handle(UpdateHubCommand command, CancellationToken cancellationToken = default)
        {
            var hub = await _managementService.UpdateHubAsync(command.Hub, cancellationToken);

            return hub;
        }

        async Task<IAggregateRoot> IRequestHandler<UpdateHubCommand, IAggregateRoot>.Handle(UpdateHubCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> ICommandHandler<UpdateHubCommand, IAggregateRoot>.Handle(UpdateHubCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}