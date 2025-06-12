using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Characters;

public class CreateCharacterSessionResult : AbstractResult
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

internal class CreateCharacterSessionHandler : IRequestHandler<CreateCharacterSessionRequest, CreateCharacterSessionResult>
{
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public CreateCharacterSessionHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<CreateCharacterSessionResult> Handle(CreateCharacterSessionRequest request, CancellationToken cancellationToken)
    {
        var character = await _readWriteDatabaseContext.Characters
            .Include(x => x.Sessions!.Where(s => s.Ended == null))
            .SingleOrDefaultAsync(x => x.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            throw new InvalidOperationException();
        }

        character.CreateSession();
        await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);

        return new CreateCharacterSessionResult(character);
    }
}
