namespace CRM_System.API.Producer
{
    public interface IRabbitMQProducer
    {
        Task SendRatesMessage<T>(T message);
    }
}
