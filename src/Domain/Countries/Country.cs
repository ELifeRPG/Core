namespace ELifeRPG.Domain.Countries;

public class Country
{
    public Guid Id { get; init; }

    public string Code { get; init; } = null!;

    public static readonly Country Default = new() { Id = Guid.Parse("90AFA7EC-CCDA-49B7-9AE4-F08B4EF4B759"), Code = "EL" };
}
