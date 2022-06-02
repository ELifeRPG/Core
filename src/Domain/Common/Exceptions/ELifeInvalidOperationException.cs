namespace ELifeRPG.Application.Common.Exceptions;

public class ELifeInvalidOperationException : InvalidOperationException
{
    public ELifeInvalidOperationException()
        : base("Invalid operation")
    {
    }

    public ELifeInvalidOperationException(string message)
        : base(message)
    {
    }

    public ELifeInvalidOperationException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public static void ThrowIf(bool condition, string message)
    {
        if (condition)
        {
            throw new ELifeInvalidOperationException(message);
        }
    }
}
