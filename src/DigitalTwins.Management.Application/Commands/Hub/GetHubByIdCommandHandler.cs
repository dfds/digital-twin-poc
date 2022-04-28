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
        private readonly IHubService _hubService;

        public GetHubByIdCommandHandler(IHubService hubService)
        {
            _hubService = hubService ?? throw new ArgumentNullException(nameof(hubService));
        }

        public async Task<HubRoot> Handle(GetHubByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _hubService.GetHubByIdAsync(command.HubId, cancellationToken);
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