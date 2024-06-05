using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Banking.Events;

public class BankAccountTransactionExecutedEvent : DomainEvent
{
    public BankAccountTransactionExecutedEvent(BankAccountTransaction transaction, Character? character = null)
    {
        Transaction = transaction;
        Character = character;
    }

    public BankAccountTransaction Transaction { get; }

    public Character? Character { get; }
}
