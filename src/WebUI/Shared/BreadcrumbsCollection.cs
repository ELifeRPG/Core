using System.Collections.ObjectModel;
using MudBlazor;

namespace ELifeRPG.Core.WebUI.Shared;

public class BreadcrumbsCollection : ObservableCollection<BreadcrumbItem>
{
    public Guid InstanceId { get; } = Guid.NewGuid();
}
