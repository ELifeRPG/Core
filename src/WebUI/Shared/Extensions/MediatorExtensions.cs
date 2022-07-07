using MediatR;
using MudBlazor;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MediatorExtensions
{
    public static async Task<(bool succeed, TResponse? response)> TrySend<TResponse>(this IMediator mediator, IRequest<TResponse> request, ISnackbar snackbar, CancellationToken cancellationToken = default)
    {
        try
        {
            return (true, await mediator.Send(request, cancellationToken));
        }
        catch (Exception exception)
        {
            snackbar.Add(exception.Message, Severity.Error);
        }

        return (false, default);
    }
}
