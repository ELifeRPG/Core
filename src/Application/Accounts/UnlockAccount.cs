using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using Mediator;

namespace ELifeRPG.Application.Accounts;

public class UnlockAccountResult : AbstractResult
{
}

public class UnlockAccountCommand : IRequest<UnlockAccountResult>
{
    public UnlockAccountCommand(Guid accountId)
    {
        AccountId = accountId;
    }

    public Guid AccountId { get; }
}

public class UnlockAccountHandler : IRequestHandler<UnlockAccountCommand, UnlockAccountResult>
{
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public UnlockAccountHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<UnlockAccountResult> Handle(UnlockAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _readWriteDatabaseContext.Accounts.FindAsync(new object?[] { request.AccountId }, cancellationToken);
        if (account is null)
        {
            throw new ELifeEntityNotFoundException("Account does not exist.");
        }

        account.Unlock();
        await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);

        return new UnlockAccountResult()
            .AddSuccessMessage($"Account `{account.Id}` has been unlocked.");
    }
}
