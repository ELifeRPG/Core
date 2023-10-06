using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Banking;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Banking.Queries;

public class BankAccountsQueryResult : AbstractResult
{
    public BankAccountsQueryResult(ICollection<BankAccount> bankAccounts)
    {
        BankAccounts = bankAccounts;
    }
    
    public ICollection<BankAccount> BankAccounts { get; }
}

public class BankAccountsQuery : IRequest<BankAccountsQueryResult>
{
    public Guid? CharacterId { get; init; }
}

internal class BankAccountsQueryHandler : IRequestHandler<BankAccountsQuery, BankAccountsQueryResult>
{
    private readonly IDatabaseContext _databaseContext;

    public BankAccountsQueryHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<BankAccountsQueryResult> Handle(BankAccountsQuery request, CancellationToken cancellationToken)
    {
        var query = _databaseContext.BankAccounts.AsQueryable();

        if (request.CharacterId is not null)
        {
            query = query.Where(x => x.Owner.Character!.Id == request.CharacterId);
        }

        var result = await query.AsNoTracking().ToListAsync(cancellationToken);

        return new BankAccountsQueryResult(result);
    }
}
