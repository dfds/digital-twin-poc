using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class DeleteHubCommandHandler : ICommandHandler<DeleteHubCommand, bool>
    {
        private readonly IHubService _hubService;

        public DeleteHubCommandHandler(IHubService hubService)
        {
            _hubService = hubService ?? throw new ArgumentNullException(nameof(hubService));
        }

        public async Task<bool> Handle(DeleteHubCommand command, CancellationToken cancellationToken = default)
        {
            return await _hubService.DeleteHubAsync(command.HubId, cancellationToken);
        }
    }
}