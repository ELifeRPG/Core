using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using MediatR;

namespace ELifeRPG.Application.Accounts;

public class UnlockAccountResult : ResultBase
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
    private readonly IDatabaseContext _databaseContext;

    public UnlockAccountHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<UnlockAccountResult> Handle(UnlockAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _databaseContext.Accounts.FindAsync(new object?[] { request.AccountId }, cancellationToken);
        if (account is null)
        {
            throw new ELifeEntityNotFoundException("Account does not exist.");
        }

        account.Unlock();
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new UnlockAccountResult()
            .AddSuccessMessage($"Account `{account.Id}` has been unlocked.");
    }
}



