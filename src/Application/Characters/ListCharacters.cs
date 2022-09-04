using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Characters;

public class ListCharactersResult : AbstractResult
{
    public ListCharactersResult(List<Character> characters)
    {
        Characters = characters;
    }
    
    public List<Character> Characters { get; }
}

public class ListCharactersQuery : IRequest<ListCharactersResult>
{
    public ListCharactersQuery()
    {
    }
    
    public ListCharactersQuery(Guid accountId)
    {
        AccountId = accountId;
    }
    
    public Guid? AccountId { get; }
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
        var query = _databaseContext.Characters.AsQueryable();

        if (request.AccountId.HasValue)
        {
            query = query.Where(x => x.Account!.Id == request.AccountId);
        }
        
        var characters = await query.OrderBy(x => x.Id).ToListAsync(cancellationToken);
        return new ListCharactersResult(characters);
    }
}
