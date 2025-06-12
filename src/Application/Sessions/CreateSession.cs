using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Sessions;

public class CreateSessionResult : AbstractResult
{
    public required Guid AccountId { get; init; }

    public required AccountStatus AccountStatus { get; init; }
}

public class CreateSessionRequest : IRequest<CreateSessionResult>
{
    public CreateSessionRequest(Guid bohemiaId)
    {
        BohemiaId = bohemiaId;
    }

    public Guid BohemiaId { get; }
}

public class CreateSessionHandler : IRequestHandler<CreateSessionRequest, CreateSessionResult>
{
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public CreateSessionHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<CreateSessionResult> Handle(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        var account = await _readWriteDatabaseContext.Accounts
            .SingleOrDefaultAsync(x => x.BohemiaId == request.BohemiaId, cancellationToken);

        if (account is null)
        {
            // temporary until ingame account linking is finished, then blocking this case -> https://github.com/ELifeRPG/Core/issues/96
            account = new Account(request.BohemiaId);
            _readWriteDatabaseContext.Accounts.Add(account);
            await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);
        }

        var response = new CreateSessionResult
        {
            AccountId = account.Id,
            AccountStatus = account.Status,
        };

        if (account.Status == AccountStatus.Locked)
        {
            response.AddErrorMessage("Account locked", "Your account is currently locked. You might contact the server owner.");
        }

        return response;
    }
}
