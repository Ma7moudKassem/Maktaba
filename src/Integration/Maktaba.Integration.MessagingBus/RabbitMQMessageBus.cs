namespace Maktaba.Integration.MessagingBus;

public class RabbitMQMessageBus : IMessageBus
{
    private readonly IBus _bus;
    public RabbitMQMessageBus(IBus bus)
    {
        _bus = bus;
    }
    public async Task PublishMessage<T>(T message, string topicName)
    {
        Uri uri = new($"rabbitmq://localhost/{message}Queue");
        var endPoint = await _bus.GetSendEndpoint(uri);

        if (message is not null)
            await endPoint.Send(message);
    }
}