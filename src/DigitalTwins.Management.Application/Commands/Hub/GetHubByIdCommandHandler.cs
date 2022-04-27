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
    public sealed class GetHubByIdCommandHandler : ICommandHandler<GetHubByIdCommand, HubRoot>, ICommandHandler<GetHubByIdCommand, IAggregateRoot>
    {
        private readonly IManagementService _managementService;

        public GetHubByIdCommandHandler(IManagementService managementService)
        {
            _managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
        }

        public async Task<HubRoot> Handle(GetHubByIdCommand command, CancellationToken cancellationToken = default)
        {
            var hub = await _managementService.GetHubById(command.HubId, cancellationToken);

            return hub;
        }

        async Task<IAggregateRoot> IRequestHandler<GetHubByIdCommand, IAggregateRoot>.Handle(GetHubByIdCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> ICommandHandler<GetHubByIdCommand, IAggregateRoot>.Handle(GetHubByIdCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}