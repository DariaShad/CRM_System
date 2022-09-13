namespace CRM_System.API.Producer
{
    public interface IRabbitMQProducer
    {
        Task SendMessage<T>(T message);
    }
}
