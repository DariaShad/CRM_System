using IncredibleBackendContracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.RabbitMQ.Consumer
{
    public class RabbitMQConsumer : IConsumer<LeadsRoleUpdatedEvent>
    {
        private readonly IReceiveEndpoint _receiveEndpoint;

        private readonly ILogger _logger;

        public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger, IReceiveEndpoint receiveEndpoint)
        {
            _receiveEndpoint = receiveEndpoint;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<LeadsRoleUpdatedEvent> context)
        {
            await context.Publish<LeadsRoleUpdatedEvent>(new
            {
                context.Message.Ids
            });
        }
    }
}
