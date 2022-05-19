using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Characters;

public class ListCharactersResult
{
    public ListCharactersResult(List<Character> characters)
    {
        Characters = characters;
    }
    
    public List<Character> Characters { get; }
}

public class ListCharactersQuery : IRequest<ListCharactersResult>
{
}

public class ListCharactersHandler : IRequestHandler<ListCharactersQuery, ListCharactersResult>
{
    private readonly IDatabaseContext _databaseContext;

    public ListCharactersHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ListCharactersResult> Handle(ListCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await _databaseContext.Characters.ToListAsync(cancellationToken);
        return new ListCharactersResult(characters);
    }
}
