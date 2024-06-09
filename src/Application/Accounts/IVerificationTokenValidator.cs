namespace ELifeRPG.Application.Accounts;

public interface IVerificationTokenValidator
{
    Task<bool> IsValid(string token);
}
