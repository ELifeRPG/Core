using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Characters;

namespace ELifeRPG.Infrastructure.Common;

public class DatabaseContextSeed
{
    public static async Task SeedSampleDataAsync(DatabaseContext context)
    {
        if (!context.Accounts.Any())
        {
            context.Accounts.Add(new Account
            {
                Id = Guid.NewGuid(),
                Characters = new List<Character>
                {
                    new() { Id = Guid.NewGuid(), Name = new CharacterName("Jon", "Doe") },
                },
            });
            
            await context.SaveChangesAsync();
        }
    }
}
