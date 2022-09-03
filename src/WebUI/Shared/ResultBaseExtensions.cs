using ELifeRPG.Application.Common;
using MudBlazor;

namespace ELifeRPG.Core.WebUI.Shared;

public static class ResultBaseExtensions
{
    public static void HandleMessages(this AbstractResult result, ISnackbar snackbar)
    {
        foreach (var message in result.Messages)
        {
            snackbar.Add($"<b>{message.Summary}</b> {message.Text}");
        }
    }
}
