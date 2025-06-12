namespace ELifeRPG.Application.Common;

public enum MessageType
{
    Information,
    Success,
    Warning,
    Error,
}

public class Message
{
    public Message(MessageType type, string summary, string? text = null)
    {
        Type = type;
        Summary = summary;
        Text = text;
    }

    public MessageType Type { get; }

    public string Summary { get; }

    public string? Text { get; }
}
