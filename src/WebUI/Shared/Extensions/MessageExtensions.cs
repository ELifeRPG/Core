using ELifeRPG.Application.Common;
using MudBlazor;

namespace ELifeRPG.Core.WebUI.Shared.Extensions;

public static class MessageExtensions
{
    public static Severity ToMudSeverity(this MessageType messageType)
    {
        return messageType switch
        {
            MessageType.Information => Severity.Info,
            MessageType.Success => Severity.Success,
            MessageType.Warning => Severity.Warning,
            MessageType.Error => Severity.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(messageType), messageType, null)
        };
    }
}
