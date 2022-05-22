namespace ELifeRPG.Core.Api.Models;

public class CharacterDto : DtoBase
{
    public string Id { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
}
