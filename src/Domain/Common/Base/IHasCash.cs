namespace ELifeRPG.Domain.Common.Base;

public interface IHasCash
{
    decimal Cash { get; init; }

    void ReceiveCash(decimal amount);
}
