using DigitalTwins.Management.Application;
using DigitalTwins.Management.Application.Commands.Hub;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("restart")]
        public async Task<string> Restart(RestartDeviceCommand command)
        {
            return await _applicationFacade.Execute(command);
        }
    }
}