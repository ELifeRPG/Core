namespace ELifeRPG.Application.Common;

public abstract class AbstractResult
{
    public List<Message> Messages { get; } = [];
}

public static class ResponseBaseExtensions
{
    public static TValue AddInformationMessage<TValue>(this TValue response, string summary, string? text = null)
        where TValue : AbstractResult
    {
        response.AddMessage(MessageType.Information, summary, text);
        return response;
    }
    
    public static TValue AddSuccessMessage<TValue>(this TValue response, string summary, string? text = null)
        where TValue : AbstractResult
    {
        response.AddMessage(MessageType.Success, summary, text);
        return response;
    }
    
    public static TValue AddWarningMessage<TValue>(this TValue response, string summary, string? text = null)
        where TValue : AbstractResult
    {
        response.AddMessage(MessageType.Warning, summary, text);
        return response;
    }
    
    public static TValue AddErrorMessage<TValue>(this TValue response, string summary, string? text = null)
        where TValue : AbstractResult
    {
        response.AddMessage(MessageType.Error, summary, text);
        return response;
    }
    
    private static TValue AddMessage<TValue>(this TValue response, MessageType type, string summary, string? text = null)
        where TValue : AbstractResult
    {
        response.Messages.Add(new Message(type, summary, text));
        return response;
    }
}
