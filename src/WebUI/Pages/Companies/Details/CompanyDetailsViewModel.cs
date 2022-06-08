using System.Collections.ObjectModel;
using ELifeRPG.Application.Companies;
using ELifeRPG.Domain.Companies;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace ELifeRPG.Core.WebUI.Pages.Companies.Details;

public class CompanyDetailsViewModel : ViewModelBase
{
    private readonly IMediator _mediator;
    private ISnackbar _snackbar = null!;
    private bool _loading;
    
    public CompanyDetailsViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public bool Loading
    {
        get => _loading;
        set => Set(ref _loading, value);
    }
    
    [Parameter]
    public string? CompanyId { get; set; }

    //[CascadingParameter(Name = "BreadcrumbItems")]
    //public ObservableCollection<BreadcrumbItem>? BreadcrumbItems { get; set; }

    public string? Name { get; set; }
    
    public ICollection<CompanyMembership>? TopMembers { get; set; }
    
    public override void OnInitialized()
    {
        Loading = true;
        _snackbar = RootServiceProvider.GetRequiredService<ISnackbar>();
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
        
        //BreadcrumbItems!.Add(new BreadcrumbItem(Name, null, disabled: true));

        Loading = false;
    }
}
