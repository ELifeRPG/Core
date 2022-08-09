using ELifeRPG.Application.Accounts;
using ELifeRPG.Domain.Banking.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ELifeRPG.Application.Banking.EventHandlers;

public class BankingLogEventHandler : 
    INotificationHandler<BankAccountOpenedEvent>
{
    private readonly ILogger<AccountLogManager> _logger;

    public BankingLogEventHandler(ILogger<AccountLogManager> logger)
    {
        _logger = logger;
    }

    public Task Handle(BankAccountOpenedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Bank-Account {AccountId} has been opened", notification.BankAccount.Id.ToString());
        return Task.CompletedTask;
    }
}
