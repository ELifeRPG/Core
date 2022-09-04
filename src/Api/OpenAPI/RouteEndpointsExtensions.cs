using Swashbuckle.AspNetCore.Annotations;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class RouteEndpointsExtensions
{
    internal static RouteHandlerBuilder WithSummary(this RouteHandlerBuilder builder, string summary, string? description = null)
    {
        return builder.WithMetadata(new SwaggerOperationAttribute(summary, description));
    }
}
