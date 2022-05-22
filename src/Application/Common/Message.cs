namespace ELifeRPG.Application.Common;

public class Message
{
    public MessageType Type { get; }
    
    public string Summary { get; }
    
    public string? Text { get; }

    public Message(MessageType type, string summary, string? text = null)
    {
        Type = type;
        Summary = summary;
        Text = text;
    }
}

public enum MessageType
{
    Information,
    Success,
    Warning,
    Error,
}
