namespace Maktaba.Integration.MessagingBus;

public class RabbitMQMessageBus : IMessageBus
{
    private readonly ISendEndpointProvider _endpointProvider;
    public RabbitMQMessageBus(ISendEndpointProvider endpointProvider)
    {
        _endpointProvider = endpointProvider;
    }

    public async Task PublishMessage<T>(T message, string queueName)
    {
        Uri uri = new($"rabbitmq://localhost/{queueName}");

        var endpoint = await _endpointProvider.GetSendEndpoint(uri);

        if (message is not null)
            await endpoint.Send(message);
    }
}