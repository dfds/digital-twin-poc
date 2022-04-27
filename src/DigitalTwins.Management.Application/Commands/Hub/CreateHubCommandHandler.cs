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
    public sealed class CreateHubCommandHandler : ICommandHandler<CreateHubCommand, HubRoot>, ICommandHandler<CreateHubCommand, IAggregateRoot>
    {
        private readonly IManagementService _managementService;

        public CreateHubCommandHandler(IManagementService managementService)
        {
            _managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
        }

        public async Task<HubRoot> Handle(CreateHubCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _managementService.AddHubAsync(command.Name, command.ConnectionString, command.Devices, cancellationToken);

            return report;
        }

        async Task<IAggregateRoot> IRequestHandler<CreateHubCommand, IAggregateRoot>.Handle(CreateHubCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> ICommandHandler<CreateHubCommand, IAggregateRoot>.Handle(CreateHubCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}