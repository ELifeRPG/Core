using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Characters;

public class CreateCharacterSessionResult : ResultBase
{
    public CreateCharacterSessionResult(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }
}

public class CreateCharacterSessionRequest : IRequest<CreateCharacterSessionResult>
{
    public CreateCharacterSessionRequest(Guid characterId)
    {
        CharacterId = characterId;
    }
    
    public Guid CharacterId { get; }
}

public class CreateCharacterSessionHandler : IRequestHandler<CreateCharacterSessionRequest, CreateCharacterSessionResult>
{
    private readonly IDatabaseContext _databaseContext;

    public CreateCharacterSessionHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<CreateCharacterSessionResult> Handle(CreateCharacterSessionRequest request, CancellationToken cancellationToken)
    {
        var character = await _databaseContext.Characters
            .Include(x => x.Sessions!.Where(s => s.Ended == null))
            .SingleOrDefaultAsync(x => x.Id == request.CharacterId, cancellationToken);
        
        if (character is null)
        {
            throw new InvalidOperationException();
        }
        
        character.CreateSession();
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new CreateCharacterSessionResult(character);
    }
}
