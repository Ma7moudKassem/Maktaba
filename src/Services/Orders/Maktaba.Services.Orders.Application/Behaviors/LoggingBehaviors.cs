namespace Maktaba.Services.Orders.Application.Behaviors;

public class LoggingBehaviors<TRequest, TResponce> : IPipelineBehavior<TRequest, TResponce>
    where TRequest : IRequest<TResponce>
{
    private readonly ILogger<LoggingBehaviors<TRequest, TResponce>> _logger;

    public LoggingBehaviors(ILogger<LoggingBehaviors<TRequest, TResponce>> logger) =>
        _logger = logger;

    public async Task<TResponce> Handle(TRequest request,
        RequestHandlerDelegate<TResponce> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling command {CommandName} ({@Command})",
            request.GetGenericTypeName(), request);

        TResponce response = await next();

        _logger.LogInformation("Command {CommandName} handled - response: {@Response}",
            request.GetGenericTypeName(), response);

        return response;
    }
}