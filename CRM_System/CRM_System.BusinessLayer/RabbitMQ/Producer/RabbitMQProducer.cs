using MassTransit;
using Microsoft.Extensions.Logging;

namespace CRM_System.API.Producer
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger _logger;

        public RabbitMQProducer(ILogger<RabbitMQProducer> logger, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task SendMessage<T>(T message)
        {
            try
            {
                _logger.LogInformation("Send to Queue");
                await _publishEndpoint.Publish(message!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
