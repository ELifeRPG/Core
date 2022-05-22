namespace ELifeRPG.Core.Api.Models;

public abstract class ResultDtoBase
{
    public List<MessageDto> Messages { get; init; } = new();
}

public class ResultResultDto<TValue> : ResultDtoBase
    where TValue : class
{
    public TValue Data { get; init; } = default!;
}
