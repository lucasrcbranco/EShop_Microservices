using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "[START] Handle request={Request} - Response={Response} - RequestData={Request}",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var requestTime = timer.Elapsed;

        if (requestTime.Seconds > 3)
        {
            logger.LogWarning(
                "[PERFORMANCE] The request {Request} took {TimeTaken}",
                typeof(TRequest).Name,
                requestTime);
        }

        logger.LogInformation(
            "[END] Handled {Request} with Response {Response}",
            typeof(TRequest).Name,
            typeof(TResponse).Name);

        return response;
    }
}
