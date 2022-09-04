using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Banking.Events;

public class BankAccountOpenedEvent : DomainEvent
{
    internal BankAccountOpenedEvent(BankAccount bankAccount)
    {
        BankAccount = bankAccount;
    }
    
    public BankAccount BankAccount { get; }
}
