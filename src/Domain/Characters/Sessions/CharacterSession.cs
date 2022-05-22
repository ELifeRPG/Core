using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Characters.Sessions;

public class CharacterSession : EntityBase
{
    private DateTime? _ended;

    public CharacterSession()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; init; }
    
    public Character? Character { get; init; }

    public DateTime? Ended
    {
        get => _ended;
        init => _ended = value;
    }

    public void End()
    {
        _ended = DateTime.UtcNow;
    }
}
