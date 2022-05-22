using ELifeRPG.Domain.Accounts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ELifeRPG.Application.Accounts;

public class AccountLogManager : INotificationHandler<AccountCreatedEvent>
{
    private readonly ILogger<AccountLogManager> _logger;

    public AccountLogManager(ILogger<AccountLogManager> logger)
    {
        _logger = logger;
    }

    public Task Handle(AccountCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Account {AccountId} has been created", notification.Account.Id.ToString());
        return Task.CompletedTask;
    }
}
