namespace ELifeRPG.Core.Api.Models;

public abstract class DtoBase
{
    public List<MessageDto> Messages { get; init; } = new();
}

public class ApiDto<TValue> : DtoBase
    where TValue : class
{
    public TValue Data { get; init; } = default!;
}
