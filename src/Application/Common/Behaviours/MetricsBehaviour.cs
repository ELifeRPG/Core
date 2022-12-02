using System.Diagnostics;
using System.Diagnostics.Metrics;
using MediatR;

namespace ELifeRPG.Application.Common.Behaviours;

public class MetricsBehaviour<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var sw = new Stopwatch();

        sw.Start();
        var result =  await next();
        sw.Stop();

        var tags = new KeyValuePair<string, object?>("request_name", request.GetType().Name);
        Metrics.RequestCounter.Add(1, tags);
        Metrics.RequestDurationHistogram.Record(sw.ElapsedMilliseconds, tags);
        
        return result;
    }
}
