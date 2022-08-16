using ELifeRPG.Application.Accounts;
using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Banking.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ELifeRPG.Application.Banking.EventHandlers;

public class BankingLogEventHandler : 
    INotificationHandler<BankAccountOpenedEvent>,
    INotificationHandler<BankAccountTransactionExecutedEvent>
{
    private readonly ILogger<AccountLogManager> _logger;
    
    /// <summary>
    /// Bank-Account {BankAccountId} has been opened
    /// </summary>
    private static readonly Action<ILogger, Guid, Exception> BankAccountOpenedEvent =
        LoggerMessage.Define<Guid>(
            LogLevel.Critical,
            new EventId((int)LoggingEventId.BankAccountOpenedEvent, nameof(LoggingEventId.BankAccountOpenedEvent)), 
            "Bank-Account {BankAccountId} has been opened");
    
    /// <summary>
    /// Bank-Account {BankAccountId} executed transaction by {CharacterId} to Bank-Account {TargetBankAccountId} with amount {TransactionAmount} (fees: {TransactionFees})
    /// </summary>
    private static readonly Action<ILogger, Guid, Guid, Guid, decimal, decimal, Exception> BankAccountTransactionExecutedEvent =
        LoggerMessage.Define<Guid, Guid, Guid, decimal, decimal>(
            LogLevel.Critical,
            new EventId((int)LoggingEventId.BankAccountTransactionExecutedEvent, nameof(LoggingEventId.BankAccountTransactionExecutedEvent)), 
            "Bank-Account {BankAccountId} executed transaction by {CharacterId} to Bank-Account {TargetBankAccountId} with amount {TransactionAmount} (fees: {TransactionFees})");
    

    public BankingLogEventHandler(ILogger<AccountLogManager> logger)
    {
        _logger = logger;
    }

    public Task Handle(BankAccountOpenedEvent notification, CancellationToken cancellationToken)
    {
        BankAccountOpenedEvent(_logger, notification.BankAccount.Id, default!);
        return Task.CompletedTask;
    }

    public Task Handle(BankAccountTransactionExecutedEvent notification, CancellationToken cancellationToken)
    {
        BankAccountTransactionExecutedEvent(
            _logger,
            notification.Transaction.Source.Id,
            notification.ExecutingCharacter.Id,
            notification.Transaction.Target!.Id,
            notification.Transaction.Amount,
            notification.Transaction.Fees,
            default!);

        return Task.CompletedTask;
    }
}
