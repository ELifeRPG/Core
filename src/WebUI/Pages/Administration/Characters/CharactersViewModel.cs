using ELifeRPG.Application.Characters;
using ELifeRPG.Core.WebUI.Shared;
using MudBlazor;

namespace ELifeRPG.Core.WebUI.Pages.Administration.Characters;

public class CharactersViewModel : ViewModelBase
{
    private readonly ISnackbar _snackbar;

    public CharactersViewModel(IServiceProvider serviceProvider, ISnackbar snackbar)
        : base(serviceProvider)
    {
        _snackbar = snackbar;
    }

    public async Task<TableData<Domain.Characters.Character>> ReloadData(TableState state)
    {
        var (succeed, result) = await TrySend(new ListCharactersQuery());
        if (!succeed)
        {
            return new TableData<Domain.Characters.Character> { TotalItems = 0, Items = new List<Domain.Characters.Character>() };
        }
        
        result!.HandleMessages(_snackbar);

        return new TableData<Domain.Characters.Character> { TotalItems = 100, Items = result!.Characters };
    }
}
