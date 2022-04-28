using DigitalTwins.Management.Application;
using DigitalTwins.Management.Application.Commands.Device;
using DigitalTwins.Management.Application.Commands.Hub;
using DigitalTwins.Management.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(ReadAccessPolicy.PolicyName)]
    public class HubController
    {
        private readonly IApplicationFacade _applicationFacade;

        public HubController(IApplicationFacade applicationFacade)
        {
            _applicationFacade = applicationFacade;
        }

        [HttpPost]
        public async Task<HubRoot> Create(CreateHubCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpGet]
        public async Task<HubRoot> Read(Guid hubId)
        {
            var command = new GetHubByIdCommand(hubId);

            return await _applicationFacade.Execute(command);
        }

        [HttpPut]
        public async Task<HubRoot> Update(UpdateHubCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpDelete]
        public async Task<bool> Delete(DeleteHubCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpPost("command")]
        public async Task<string> Command(SendDeviceCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpPost("twin")]
        public async Task<string> Query(GetDeviceTwinJsonCommand command)
        {
            return await _applicationFacade.Execute(command);
        }
    }
}