using ELifeRPG.Application.Accounts;

namespace ELifeRPG.Infrastructure.Accounts;

public class VerificationTokenValidator : IVerificationTokenValidator
{
    public Task<bool> IsValid(string token)
    {
        return Task.FromResult(false);
    }
}
