using ELifeRPG.Application.Accounts;
using ELifeRPG.Domain.Accounts;
using MediatR;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace ELifeRPG.Core.WebUI.Pages.Administration.Accounts;

public class AccountsViewModel : ViewModelBase
{
    private readonly IMediator _mediator;
    private ISnackbar? _snackbar;

    public AccountsViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void OnInitialized()
    {
        _snackbar = RootServiceProvider.GetRequiredService<ISnackbar>();
    }
    
    public async Task<TableData<Domain.Accounts.Account>> ReloadData(TableState state)
    {
        var result = await _mediator.Send(new ListAccountsQuery());
        return new TableData<Account> { TotalItems = 100, Items = result.Accounts };
    }
    
    public async Task OnLockClicked(Guid accountId)
    {
        await _mediator.Send(new LockAccountCommand(accountId));
    }
    
    public async Task OnUnlockClicked(Guid accountId)
    {
        await _mediator.Send(new UnlockAccountCommand(accountId));
    }
}
