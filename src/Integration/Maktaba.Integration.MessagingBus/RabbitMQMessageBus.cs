namespace Maktaba.Integration.MessagingBus;

public class RabbitMQMessageBus : IMessageBus
{
    private readonly ISendEndpointProvider _endpointProvider;
    private readonly IConfiguration _configuration;
    public RabbitMQMessageBus(
        ISendEndpointProvider endpointProvider,
        IConfiguration configuration)
    {
        _endpointProvider = endpointProvider;
        _configuration = configuration;
    }

    public async Task PublishMessage<T>(T message, string queueName)
    {
        Uri uri = new($"{_configuration["RabbitMQ:HostAddress"]}/{queueName}");

        var endpoint = await _endpointProvider.GetSendEndpoint(uri);

        if (message is not null)
            await endpoint.Send(message);
    }
}