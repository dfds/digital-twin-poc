using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Events;
using DigitalTwins.Management.Domain.Events.Hub;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Events.Report
{
    public sealed class HubCreatedEventHandler : IEventHandler<DeviceCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public HubCreatedEventHandler(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeviceCreatedEvent @event, CancellationToken cancellationToken = default)
        { 
            //TODO: Verify mapping
            var integrationEvent = _mapper.Map<HubCreatedIntegrationEvent>(@event);

            await _mediator.Publish(integrationEvent, cancellationToken);
        }
    }
}