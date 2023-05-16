namespace Maktaba.Integration.MessagingBus;

public interface IMessageBus
{
    Task PublishMessage<T>(T message, string queueName);
}