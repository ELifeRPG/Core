using System.Collections.ObjectModel;
using ELifeRPG.Application.Companies;
using ELifeRPG.Core.WebUI.Shared;
using ELifeRPG.Domain.Companies;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ELifeRPG.Core.WebUI.Pages.Companies.Details;

public class CompanyDetailsViewModel : PageViewModelBase
{
    private readonly IMediator _mediator;
    private readonly ObservableCollection<BreadcrumbItem> _breadcrumbs;
    private ISnackbar _snackbar = null!;
    private bool _loading;
    
    public CompanyDetailsViewModel(IMediator mediator, ObservableCollection<BreadcrumbItem> breadcrumbs)
    {
        _mediator = mediator;
        _breadcrumbs = breadcrumbs;
    }

    public bool Loading
    {
        get => _loading;
        set => Set(ref _loading, value);
    }
    
    [Parameter]
    public string? CompanyId { get; set; }

    public string? Name { get; set; }
    
    public ICollection<CompanyMembership>? TopMembers { get; set; }
    
    public override void OnInitialized()
    {
        Loading = true;
        _snackbar = RootServiceProvider.GetRequiredService<ISnackbar>();
        
        _breadcrumbs.Clear();
        _breadcrumbs.Add(new BreadcrumbItem(null, icon: Icons.Material.Filled.Home, href: "/"));
        _breadcrumbs.Add(new BreadcrumbItem("Companies", href: "/companies"));
    }

    public override async Task OnParametersSetAsync()
    {
        var (succeed, result) = await _mediator.TrySend(new GetCompanyQuery(Guid.Parse(CompanyId!)), _snackbar);
        if (!succeed)
        {
            return;
        }

        Name = result!.Company.Name;
        TopMembers = result.TopMembers;

        _breadcrumbs.Add(new BreadcrumbItem(Name, href: $"/companies/{CompanyId}"));

        Loading = false;
    }
}
