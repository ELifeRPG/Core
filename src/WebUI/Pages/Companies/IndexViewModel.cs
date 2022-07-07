using ELifeRPG.Application.Companies;
using ELifeRPG.Core.WebUI.Shared;
using ELifeRPG.Domain.Companies;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ELifeRPG.Core.WebUI.Pages.Companies;

public class IndexViewModel : ViewModelBase
{
    private readonly IMediator _mediator;
    private NavigationManager _navigationManager = null!;
    private ISnackbar _snackbar = null!;
    private bool _tableIsLoading;

    public IndexViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public bool TableIsLoading
    {
        get => _tableIsLoading;
        private set => Set(ref _tableIsLoading, value);
    }

    public override void OnInitialized()
    {
        _snackbar = RootServiceProvider.GetRequiredService<ISnackbar>();
        _navigationManager = RootServiceProvider.GetRequiredService<NavigationManager>();
        
        var breadcrumbs = RootServiceProvider.GetRequiredService<BreadcrumbsCollection>();
        breadcrumbs.Clear();
        breadcrumbs.Add(new BreadcrumbItem(null, icon: Icons.Material.Filled.Home, href: "/"));
        breadcrumbs.Add(new BreadcrumbItem("Companies", href: null, disabled: true));
    }

    public async Task<TableData<Company>> ReloadData(TableState state)
    {
        TableIsLoading = true;

        var (succeed, result) = await _mediator.TrySend(new ListCompaniesQuery(), _snackbar);
        if (succeed)
        {
            return new TableData<Company> { TotalItems = 100, Items = result!.Companies };
        }

        TableIsLoading = false;

        return new TableData<Company> { TotalItems = 100, Items = Enumerable.Empty<Company>() };
    }
    
    public void ViewCompany(Guid id)
    {
        _navigationManager.NavigateTo($"/companies/{id}");
    }
}
