namespace ELifeRPG.Application.Common.Exceptions;

public class ELifeEntityNotFoundException : Exception
{
    public ELifeEntityNotFoundException()
        : base("Entity not found")
    {
    }

    public ELifeEntityNotFoundException(string message)
        : base(message)
    {
    }

    public ELifeEntityNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
