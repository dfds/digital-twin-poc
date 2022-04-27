using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class DeleteHubCommandHandler : ICommandHandler<DeleteHubCommand, bool>
    {
        private readonly IManagementService _managementService;

        public DeleteHubCommandHandler(IManagementService managementService)
        {
            _managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
        }

        public async Task<bool> Handle(DeleteHubCommand command, CancellationToken cancellationToken = default)
        {
            var hub = await _managementService.DeleteHubAsync(command.HubId, cancellationToken);

            return hub;
        }
    }
}