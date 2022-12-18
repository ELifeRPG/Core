using System.Diagnostics;
using MediatR;

namespace ELifeRPG.Application.Common.Behaviours;

public class MetricsBehaviour<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        var sw = new Stopwatch();

        using var activity = Activities.Source.StartActivity(requestName);
        
        sw.Start();
        var result =  await next();
        sw.Stop();

        var tags = new KeyValuePair<string, object?>("request_name", requestName);
        Metrics.RequestCounter.Add(1, tags);
        Metrics.RequestDurationHistogram.Record(sw.ElapsedMilliseconds, tags);
        
        return result;
    }
}
