using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Accounts;

public class UserSignedInResult : AbstractResult
{
}

public class UserSignedInRequest : IRequest<UserSignedInResult>
{
    public UserSignedInRequest(long discordId, string accountName)
    {
        DiscordId = discordId;
        AccountName = accountName;
    }

    public long DiscordId { get; }

    public string AccountName { get; }
}

public class UserSignedInHandler : IRequestHandler<UserSignedInRequest, UserSignedInResult>
{
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public UserSignedInHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<UserSignedInResult> Handle(UserSignedInRequest request, CancellationToken cancellationToken)
    {
        var account = await _readWriteDatabaseContext.Accounts.SingleOrDefaultAsync(x => x.DiscordId == request.DiscordId, cancellationToken);
        if (account is null)
        {
            account = new Account(request.DiscordId);
            _readWriteDatabaseContext.Accounts.Add(account);
            await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);
        }

        return new UserSignedInResult();
    }
}
