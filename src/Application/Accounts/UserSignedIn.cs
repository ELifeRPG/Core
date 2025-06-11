using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using MediatR;
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
    private readonly IDatabaseContext _databaseContext;

    public UserSignedInHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<UserSignedInResult> Handle(UserSignedInRequest request, CancellationToken cancellationToken)
    {
        var account = await _databaseContext.Accounts.SingleOrDefaultAsync(x => x.DiscordId == request.DiscordId, cancellationToken);
        if (account is null)
        {
            account = new Account(request.DiscordId);
            _databaseContext.Accounts.Add(account);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        
        return new UserSignedInResult();
    }
}
