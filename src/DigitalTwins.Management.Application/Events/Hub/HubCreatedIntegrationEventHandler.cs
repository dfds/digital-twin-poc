using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Events;
using Confluent.Kafka;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Events.Report
{
    public class HubCreatedIntegrationEventHandler : IEventHandler<HubCreatedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        private readonly IProducer<Ignore, IIntegrationEvent> _producer;

        public HubCreatedIntegrationEventHandler(IMapper mapper, IProducer<Ignore, IIntegrationEvent> producer = default)
        {
            _mapper = mapper;
            _producer = producer;
        }

        public async Task Handle(HubCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            //TODO: inject target topic via IOptions
            await _producer.ProduceAsync("targetTopic", new Message<Ignore, IIntegrationEvent>() { Value = notification }, cancellationToken);
        }
    }
}