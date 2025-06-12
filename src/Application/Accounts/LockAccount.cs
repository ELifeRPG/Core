using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using Mediator;

namespace ELifeRPG.Application.Accounts;

public class LockAccountResult : AbstractResult
{
}

public class LockAccountCommand : IRequest<LockAccountResult>
{
    public LockAccountCommand(Guid accountId)
    {
        AccountId = accountId;
    }

    public Guid AccountId { get; }
}

public class LockAccountHandler : IRequestHandler<LockAccountCommand, LockAccountResult>
{
    private readonly IDatabaseContext _databaseContext;

    public LockAccountHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async ValueTask<LockAccountResult> Handle(LockAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _databaseContext.Accounts.FindAsync([request.AccountId], cancellationToken);
        if (account is null)
        {
            throw new ELifeEntityNotFoundException("Account does not exist.");
        }

        account.Lock();
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new LockAccountResult()
            .AddSuccessMessage($"Account `{account.Id}` has been locked.");
    }
}
