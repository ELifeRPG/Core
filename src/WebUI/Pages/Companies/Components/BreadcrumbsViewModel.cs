using ELifeRPG.Core.WebUI.Shared;

namespace ELifeRPG.Core.WebUI.Pages.Companies.Components;

public class BreadcrumbsViewModel : ViewModelBase
{
    public BreadcrumbsCollection Items { get; private set; } = null!;

    public override void OnInitialized()
    {
        Items = RootServiceProvider.GetRequiredService<BreadcrumbsCollection>();
    }
}
