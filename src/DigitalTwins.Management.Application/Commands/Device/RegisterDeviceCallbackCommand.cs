using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Commands.Hub
{
    public sealed class RegisterDeviceCallbackCommand : ICommand<Task>
    {
        public Guid HubId { get; init; }

        public string DeviceId { get; init; }

        public string Method { get; init; }

        public Action Callback { get; init; }

        public RegisterDeviceCallbackCommand(Guid hubId, string deviceId, string method, Action callback)
        {
            HubId = hubId;
            DeviceId = deviceId;
            Method = method;
            Callback = callback;
        }
    }
}