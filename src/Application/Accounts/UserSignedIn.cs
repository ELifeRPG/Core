using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Accounts;

public class UserSignedInResult : ResultBase
{
}

public class UserSignedInRequest : IRequest<UserSignedInResult>
{
    public UserSignedInRequest(long steamId, string accountName)
    {
        SteamId = steamId;
        AccountName = accountName;
    }
    
    public long SteamId { get; }
    
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
        var account = await _databaseContext.Accounts.SingleOrDefaultAsync(x => x.SteamId == request.SteamId, cancellationToken);
        if (account is null)
        {
            account = Account.Create(request.SteamId);
            _databaseContext.Accounts.Add(account);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        
        return new UserSignedInResult();
    }
}
