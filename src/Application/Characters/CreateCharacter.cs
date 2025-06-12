using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using Mediator;

namespace ELifeRPG.Application.Characters;

public class CreateCharacterResult : AbstractResult
{
    public CreateCharacterResult(Character character)
    {
        Character = character;
    }

    public Character Character { get; }
}

public class CreateCharacterRequest : IRequest<CreateCharacterResult>
{
    public CreateCharacterRequest(Character characterInfo)
    {
        CharacterInfo = characterInfo;
    }

    public Character CharacterInfo { get; }
}

public class CreateCharacterHandler : IRequestHandler<CreateCharacterRequest, CreateCharacterResult>
{
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public CreateCharacterHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<CreateCharacterResult> Handle(CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var character = new Character(request.CharacterInfo);
        _readWriteDatabaseContext.Characters.Add(character);
        await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);
        return new CreateCharacterResult(character);
    }
}
