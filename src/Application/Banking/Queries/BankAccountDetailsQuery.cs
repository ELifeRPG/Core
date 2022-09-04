using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Banking;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Banking.Queries;

public class BankAccountDetailsQueryResult : AbstractResult
{
    public BankAccountDetailsQueryResult(BankAccount bankAccount)
    {
        BankAccount = bankAccount;
    }
    
    public BankAccount BankAccount { get; }
}

public class BankAccountDetailsQuery : IRequest<BankAccountDetailsQueryResult>
{
    public BankAccountDetailsQuery(Guid bankAccountId)
    {
        BankAccountId = bankAccountId;
    }
    
    public Guid BankAccountId { get; }
}

internal class BankAccountDetailsQueryHandler : IRequestHandler<BankAccountDetailsQuery, BankAccountDetailsQueryResult>
{
    private readonly IDatabaseContext _databaseContext;

    public BankAccountDetailsQueryHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<BankAccountDetailsQueryResult> Handle(BankAccountDetailsQuery request, CancellationToken cancellationToken)
    {
        var bankAccount = await _databaseContext.BankAccounts.AsQueryable()
            .Include(x => x.Bookings!.OrderByDescending(b => b.Created).Take(30))
            .SingleOrDefaultAsync(x => x.Id == request.BankAccountId, cancellationToken: cancellationToken);

        if (bankAccount is null)
        {
            throw new ELifeEntityNotFoundException();
        }

        return new BankAccountDetailsQueryResult(bankAccount);
    }
}
