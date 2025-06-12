using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Characters;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace ELifeRPG.Application.Characters;

[GenerateOneOf]
public partial class ListCharactersResponse : OneOfBase<List<Character>>;

public class CharactersQuery(Guid accountId) : IRequest<ListCharactersResponse>
{
    public Guid? AccountId { get; } = accountId;
}

public class ListCharactersHandler(IDatabaseContext databaseContext)
    : IRequestHandler<CharactersQuery, ListCharactersResponse>
{
    public async ValueTask<ListCharactersResponse> Handle(CharactersQuery request, CancellationToken cancellationToken)
    {
        var query = databaseContext.Characters
            .AsNoTracking()
            .AsQueryable();

        if (request.AccountId.HasValue)
        {
            query = query.Where(x => x.Account!.Id == request.AccountId);
        }

        var characters = await query
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new ListCharactersResponse(characters);
    }
}
