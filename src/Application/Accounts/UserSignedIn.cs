using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Results;
using ELifeRPG.Domain.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace ELifeRPG.Application.Accounts;

[GenerateOneOf]
public partial class UserSignedInResult : OneOfBase<SuccessResult, FailureResult>;

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
            return new FailureResult();
            
            account = new Account(request.DiscordId);
            _databaseContext.Accounts.Add(account);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        
        return account.IsVerified
            ? new SuccessResult()
            : new FailureResult();
    }
}
