using DigitalTwins.Management.Application;
using DigitalTwins.Management.Application.Commands.Device;
using DigitalTwins.Management.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(ReadAccessPolicy.PolicyName)]
    public class DeviceController
    {
        private readonly IApplicationFacade _applicationFacade;

        public DeviceController(IApplicationFacade applicationFacade)
        {
            _applicationFacade = applicationFacade;
        }

        [HttpPost]
        public async Task<DeviceRoot> Create(CreateDeviceCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpGet]
        public async Task<DeviceRoot> Read(Guid deviceId)
        {
            var command = new GetDeviceByIdCommand(deviceId);

            return await _applicationFacade.Execute(command);
        }

        [HttpPut]
        public async Task<DeviceRoot> Update(UpdateDeviceCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpDelete]
        public async Task<bool> Delete(DeleteDeviceCommand command)
        {
            return await _applicationFacade.Execute(command);
        }
    }
}