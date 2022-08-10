using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Banking.Events;

public class BankAccountTransactionExecutedEvent : DomainEvent
{
    public BankAccountTransactionExecutedEvent(BankAccountTransaction transaction, Character executingCharacter)
    {
        Transaction = transaction;
        ExecutingCharacter = executingCharacter;
    }

    public BankAccountTransaction Transaction { get; }

    public Character ExecutingCharacter { get; }
}
