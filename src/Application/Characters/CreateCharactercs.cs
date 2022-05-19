﻿using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using MediatR;

namespace ELifeRPG.Application.Characters;

public class CreateCharacterResponse
{
    public CreateCharacterResponse(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }
}

public class CreateCharacterCommand : IRequest<CreateCharacterResponse>
{
    public CreateCharacterCommand(Character characterInfo)
    {
        CharacterInfo = characterInfo;
    }
    
    public Character CharacterInfo { get; }
}

public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, CreateCharacterResponse>
{
    private readonly IDatabaseContext _databaseContext;

    public CreateCharacterHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<CreateCharacterResponse> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = Character.Create(request.CharacterInfo);
        _databaseContext.Characters.Add(character);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return new CreateCharacterResponse(character);
    }
}
