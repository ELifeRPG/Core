namespace ELifeRPG.Core.Api.Models;

public class MessageDto
{
    public MessageTypeDto Type { get; init; }

    public string Summary { get; init; } = null!;
    
    public string? Text { get; init; }
}

public enum MessageTypeDto
{
    Information,
    Success,
    Warning,
    Error,
}
