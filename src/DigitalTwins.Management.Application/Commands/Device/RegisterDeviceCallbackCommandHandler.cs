using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Domain.Services;
using Microsoft.Azure.Devices.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class RegisterDeviceCallbackCommandHandler : ICommandHandler<RegisterDeviceCallbackCommand, Task>
    {
        private readonly IManagementService _managementService;

        public RegisterDeviceCallbackCommandHandler(IManagementService managementService)
        {
            _managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
        }

        public async Task<Task> Handle(RegisterDeviceCallbackCommand command, CancellationToken cancellationToken = default)
        {
            var hub = await _managementService.GetHubByIdAsync(command.HubId, cancellationToken);
            var device = hub.Devices.SingleOrDefault(d => d.DeviceId == command.DeviceId);

            if (device == null)
            {
                throw new ApplicationException(string.Format("Device with id: {0} not found in Hub with id: {1}", command.DeviceId, command.HubId));
            }

            var client = DeviceClient.CreateFromConnectionString(device.ConnectionString, TransportType.Mqtt);

            Task<MethodResponse> callbackMethod(MethodRequest request, object userContext)
            {
                //TODO: Verify that delegate is called
                command.Callback();

                var result = "{\"result\":\"" + command.Method + " started.\"}";

                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
            }

            await client.SetMethodHandlerAsync(command.Method, callbackMethod, null, cancellationToken);

            return Task.CompletedTask;
        }
    }
}