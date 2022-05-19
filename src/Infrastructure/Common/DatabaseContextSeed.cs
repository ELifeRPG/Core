using ELifeRPG.Domain.Characters;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Infrastructure.Common;

public class DatabaseContextSeed
{
    public static async Task SeedSampleDataAsync(DatabaseContext context)
    {
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
        
        if (!context.Characters.Any())
        {
            context.Characters.Add(new Character
            {
                Id = Guid.NewGuid(),
                Name = new CharacterName("Jon", "Doe")
            });

            await context.SaveChangesAsync();
        }
    }
}
