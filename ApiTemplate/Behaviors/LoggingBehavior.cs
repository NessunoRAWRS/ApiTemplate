using System.Diagnostics;
using MediatR;

namespace ApiTemplate.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var start = Stopwatch.GetTimestamp();
        TResponse? response;

        try
        {
            response = await next();
        }
        finally
        {
            var delta = Stopwatch.GetElapsedTime(start);
            _logger.LogInformation("{RequestName} took {ElapsedMs}ms", typeof(TRequest).Name, delta);
        }

        return response;
    }
}