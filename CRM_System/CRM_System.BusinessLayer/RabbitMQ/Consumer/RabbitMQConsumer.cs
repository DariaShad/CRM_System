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
        private readonly ILeadsService _leadsService;

        private readonly ILogger _logger;

        public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger, ILeadsService leadsService)
        {
            _leadsService = leadsService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<LeadsRoleUpdatedEvent> context)
        {
            var ids = context.Message.Ids;
            await _leadsService.UpdateRole(ids);
        }
    }
}
