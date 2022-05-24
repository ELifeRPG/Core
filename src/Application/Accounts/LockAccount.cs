using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using MediatR;

namespace ELifeRPG.Application.Accounts;

public class LockAccountResult : ResultBase
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

    public async Task<LockAccountResult> Handle(LockAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _databaseContext.Accounts.FindAsync(new object?[] { request.AccountId }, cancellationToken);
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



