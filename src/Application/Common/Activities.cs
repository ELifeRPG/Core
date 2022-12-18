using System.Diagnostics;

namespace ELifeRPG.Application.Common;

public static class Activities
{
    public static readonly string SourceName = "ELifeRPG";
    
    public static readonly ActivitySource Source = new(SourceName);
}
