using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Sessions;

public class CreateSessionResponse : ResponseBase
{
    public CreateSessionResponse(Guid accountId)
    {
        AccountId = accountId;
    }
    
    public Guid AccountId { get; }
}

public class CreateSessionRequest : IRequest<CreateSessionResponse>
{
    public CreateSessionRequest(string enfusionIdentifier)
    {
        EnfusionIdentifier = enfusionIdentifier;
    }
    
    public string EnfusionIdentifier { get; }
}

public class CreateSessionHandler : IRequestHandler<CreateSessionRequest, CreateSessionResponse>
{
    private readonly IDatabaseContext _databaseContext;

    public CreateSessionHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<CreateSessionResponse> Handle(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        var account = await _databaseContext.Accounts
            .SingleOrDefaultAsync(x => x.EnfusionIdentifier == request.EnfusionIdentifier, cancellationToken);

        if (account is null)
        {
            account = Account.Create(Guid.NewGuid(), request.EnfusionIdentifier);
            _databaseContext.Accounts.Add(account);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        var response = new CreateSessionResponse(account.Id);

        if (account.Status == AccountStatus.Locked)
        {
            response.AddErrorMessage("Account locked", "Your account is currently locked. You might contact the server owner.");
        }

        return response;
    }
}
