using System.Diagnostics.Metrics;

namespace ELifeRPG.Application.Common;

public static class Metrics
{
    public static readonly Meter Meter = new("ELifeRPG");
    
    public static readonly Counter<int> RequestCounter = Meter.CreateCounter<int>("request_counter");
    public static readonly Histogram<float> RequestDurationHistogram = Meter.CreateHistogram<float>("request_duration", unit: "ms");
}
