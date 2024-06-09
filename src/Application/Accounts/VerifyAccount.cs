namespace ELifeRPG.Application.Accounts;

public record Verified : SuccessResult;

public record AlreadyVerified : SuccessResult;

public record VerificationFailed : FailureResult;

[GenerateOneOf]
public partial class VerifyAccountResult : OneOfBase<Verified, AlreadyVerified, VerificationFailed>;

public record VerifyAccountRequest : IRequest<VerifyAccountResult>
{
    public required long DiscordId { get; init; }
    
    public required string Token { get; init; }
}

public class VerifyAccountHandler(IDatabaseContext databaseContext, IVerificationTokenValidator tokenValidator) : IRequestHandler<VerifyAccountRequest, VerifyAccountResult>
{
    public async Task<VerifyAccountResult> Handle(VerifyAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await databaseContext.Accounts.SingleOrDefaultAsync(x => x.DiscordId == request.DiscordId, cancellationToken);

        if (account is null)
        {
            if (await tokenValidator.IsValid(request.Token))
            {
                return new Verified()
                    .AddSuccessMessage("Hurray");
            }
            
            return new VerificationFailed()
                .AddErrorMessage("Token invalid");
        }

        if (account.IsVerified)
        {
            return new AlreadyVerified();
        }

        throw new InvalidOperationException("WTF");
    }
}
