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
    private readonly IDatabaseContext _databaseContext;

    public CreateCharacterHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async ValueTask<CreateCharacterResult> Handle(CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var character = new Character(request.CharacterInfo);
        _databaseContext.Characters.Add(character);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return new CreateCharacterResult(character);
    }
}
