namespace ELifeRPG.Core.Api.Models;

public enum MessageTypeEnumDto
{
    Information,
    Success,
    Warning,
    Error,
}

public class MessageDto
{
    public MessageTypeEnumDto Type { get; init; }

    public string Summary { get; init; } = null!;

    public string? Text { get; init; }
}
