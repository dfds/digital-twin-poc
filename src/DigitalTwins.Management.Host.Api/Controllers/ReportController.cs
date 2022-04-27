using CostJanitor.Application;
using CostJanitor.Application.Commands.Report;
using CostJanitor.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostJanitor.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(ReadAccessPolicy.PolicyName)]
    public class ReportController
    {
        private readonly IApplicationFacade _applicationFacade;

        public ReportController(IApplicationFacade applicationFacade)
        {
            _applicationFacade = applicationFacade;
        }

        [HttpGet("identifier")]
        public async Task<IEnumerable<ReportRoot>> GetByIdentifier(string capabilityIdentifier)
        {
            var command = new GetReportByCapabilityIdentifierCommand(capabilityIdentifier);

            return await GetByIdentifierCommand(command);
        }

        [HttpGet("command/identifier")]
        public async Task<IEnumerable<ReportRoot>> GetByIdentifierCommand(GetReportByCapabilityIdentifierCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpGet("dateRange")]
        public async Task<IEnumerable<ReportRoot>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var command = new GetReportByDateRangeCommand(startDate, endDate);

            return await GetByDateRangeCommand(command);
        }

        [HttpGet("command/dateRange")]
        public async Task<IEnumerable<ReportRoot>> GetByDateRangeCommand(GetReportByDateRangeCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpPost]
        public async Task<ReportRoot> Create(CreateReportCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpPut]
        public async Task<ReportRoot> Update(UpdateReportCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpDelete]
        public async Task<bool> Delete(DeleteReportCommand command)
        {
            return await _applicationFacade.Execute(command);
        }
    }
}