using ELifeRPG.Application.Accounts;
using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Banking.Events;
using Mediator;
using Microsoft.Extensions.Logging;

namespace ELifeRPG.Application.Banking.EventHandlers;

public class BankingLogEventHandler :
    INotificationHandler<BankAccountOpenedEvent>,
    INotificationHandler<BankAccountTransactionExecutedEvent>
{
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

    private readonly ILogger<AccountLogManager> _logger;

    public BankingLogEventHandler(ILogger<AccountLogManager> logger)
    {
        _logger = logger;
    }

    public ValueTask Handle(BankAccountOpenedEvent notification, CancellationToken cancellationToken)
    {
        BankAccountOpenedEvent(_logger, notification.BankAccount.Id, default!);
        return ValueTask.CompletedTask;
    }

    public ValueTask Handle(BankAccountTransactionExecutedEvent notification, CancellationToken cancellationToken)
    {
        switch (notification.Transaction.Type)
        {
            case BankAccountTransactionType.BankTransfer:
                BankAccountTransactionExecutedEvent(
                    _logger,
                    notification.Transaction.BankAccount.Id,
                    notification.Character?.Id ?? default,
                    notification.Transaction.Source!.Id,
                    notification.Transaction.Amount,
                    notification.Transaction.Fees,
                    default!);
                break;
            case BankAccountTransactionType.CashDeposit:
                break;
            case BankAccountTransactionType.CashWithdrawal:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(notification), nameof(notification.Transaction.Type));
        }

        return ValueTask.CompletedTask;
    }
}
