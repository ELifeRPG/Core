using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Accounts;

public class ListAccountsResult : ResultBase
{
    public ListAccountsResult(List<Account> accounts)
    {
        Accounts = accounts;
    }
    
    public List<Account> Accounts { get; }
}

public class ListAccountsQuery : IRequest<ListAccountsResult>
{
}

public class ListAccountsHandler : IRequestHandler<ListAccountsQuery, ListAccountsResult>
{
    private readonly IDatabaseContext _databaseContext;

    public ListAccountsHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ListAccountsResult> Handle(ListAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _databaseContext.Accounts
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
        
        return new ListAccountsResult(accounts);
    }
}
