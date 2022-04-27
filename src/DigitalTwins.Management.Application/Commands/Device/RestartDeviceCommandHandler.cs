using CloudEngineering.CodeOps.Abstractions.Commands;
using DigitalTwins.Management.Application.Extensions;
using Microsoft.Azure.Devices;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class RestartDeviceCommandHandler : ICommandHandler<RestartDeviceCommand, string>
    {
        public async Task<string> Handle(RestartDeviceCommand command, CancellationToken cancellationToken = default)
        {
            using var serviceClient = command.Hub.GetServiceClient();
            var method = new CloudToDeviceMethod("reboot")
            {
                ResponseTimeout = TimeSpan.FromSeconds(30)
            };

            var result = await serviceClient.InvokeDeviceMethodAsync(command.Device.DeviceId, method, cancellationToken);
            
            return result.GetPayloadAsJson();
        }
    }
}