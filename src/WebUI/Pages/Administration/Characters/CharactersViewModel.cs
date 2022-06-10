using ELifeRPG.Application.Characters;
using ELifeRPG.Core.WebUI.Shared;
using MediatR;
using MudBlazor;

namespace ELifeRPG.Core.WebUI.Pages.Administration.Characters;

public class CharactersViewModel : PageViewModelBase
{
    private readonly IMediator _mediator;
    private ISnackbar? _snackbar;

    public CharactersViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void OnInitialized()
    {
        _snackbar = RootServiceProvider.GetRequiredService<ISnackbar>();
    }

    public async Task<TableData<Domain.Characters.Character>> ReloadData(TableState state)
    {
        var (succeed, result) = await _mediator.TrySend(new ListCharactersQuery(), _snackbar!);
        if (!succeed)
        {
            return new TableData<Domain.Characters.Character> { TotalItems = 0, Items = new List<Domain.Characters.Character>() };
        }
        
        result!.HandleMessages(_snackbar!);

        return new TableData<Domain.Characters.Character> { TotalItems = 100, Items = result!.Characters };
    }
}
