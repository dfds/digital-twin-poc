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
        private readonly IHubService _hubService;

        public CreateHubCommandHandler(IHubService hubService)
        {
            _hubService = hubService ?? throw new ArgumentNullException(nameof(hubService));
        }

        public async Task<HubRoot> Handle(CreateHubCommand command, CancellationToken cancellationToken = default)
        {
            return await _hubService.AddHubAsync(command.Name, command.ConnectionString, command.Devices, cancellationToken);
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