using MediatR;
using MudBlazor;

namespace ELifeRPG.Core.WebUI.Shared;

public abstract class ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    protected ViewModelBase(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected async Task<(bool succeed, TResponse? response)> TrySend<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        
        try
        {
            return (true, await mediator.Send(request, cancellationToken));
        }
        catch (Exception exception)
        {
            var snackbar = _serviceProvider.GetRequiredService<ISnackbar>();
            snackbar.Add(exception.Message, Severity.Error);
        }

        return (false, default);
    }
}
