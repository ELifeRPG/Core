using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Sessions;

public class CreateSessionResult : ResultBase
{
    public CreateSessionResult(Guid accountId)
    {
        AccountId = accountId;
    }
    
    public Guid AccountId { get; }
}

public class CreateSessionRequest : IRequest<CreateSessionResult>
{
    public CreateSessionRequest(string enfusionIdentifier)
    {
        EnfusionIdentifier = enfusionIdentifier;
    }
    
    public string EnfusionIdentifier { get; }
}

public class CreateSessionHandler : IRequestHandler<CreateSessionRequest, CreateSessionResult>
{
    private readonly IDatabaseContext _databaseContext;

    public CreateSessionHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<CreateSessionResult> Handle(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        var account = await _databaseContext.Accounts
            .SingleOrDefaultAsync(x => x.EnfusionIdentifier == request.EnfusionIdentifier, cancellationToken);

        if (account is null)
        {
            account = Account.Create(Guid.NewGuid(), request.EnfusionIdentifier);
            _databaseContext.Accounts.Add(account);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        var response = new CreateSessionResult(account.Id);

        if (account.Status == AccountStatus.Locked)
        {
            response.AddErrorMessage("Account locked", "Your account is currently locked. You might contact the server owner.");
        }

        return response;
    }
}
