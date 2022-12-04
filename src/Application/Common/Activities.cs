using System.Diagnostics;

namespace ELifeRPG.Application.Common;

public static class Activities
{
    public const string SourceName = "ELifeRPG";
    
    public static readonly ActivitySource Source = new(SourceName);
}
