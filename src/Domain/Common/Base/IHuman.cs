namespace ELifeRPG.Domain.Common.Base;

public interface IHuman : IHasCash
{
    bool PayCash(decimal amount, IHasCash receiver);
}
