using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Characters;

public class CreateCharacterSessionResponse
{
    public CreateCharacterSessionResponse(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }
}

public class CreateCharacterSessionRequest : IRequest<CreateCharacterSessionResponse>
{
    public CreateCharacterSessionRequest(Guid characterId)
    {
        CharacterId = characterId;
    }
    
    public Guid CharacterId { get; }
}

public class CreateCharacterSessionHandler : IRequestHandler<CreateCharacterSessionRequest, CreateCharacterSessionResponse>
{
    private readonly IDatabaseContext _databaseContext;

    public CreateCharacterSessionHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<CreateCharacterSessionResponse> Handle(CreateCharacterSessionRequest request, CancellationToken cancellationToken)
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

        return new CreateCharacterSessionResponse(character);
    }
}
