using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Banking;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Banking.Queries;

public class BanksQueryResult : AbstractResult
{
    public BanksQueryResult(ICollection<Bank> banks)
    {
        Banks = banks;
    }

    public ICollection<Bank> Banks { get; }
}

public class BanksQuery : IRequest<BanksQueryResult>
{
    public Guid? BankId { get; init; }
}

internal class BanksQueryHandler : IRequestHandler<BanksQuery, BanksQueryResult>
{
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public BanksQueryHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<BanksQueryResult> Handle(BanksQuery request, CancellationToken cancellationToken)
    {
        var query = _readWriteDatabaseContext.Banks.AsQueryable();

        if (request.BankId is not null)
        {
            query = query.Where(x => x.Id == request.BankId);
        }

        var result = await query.AsNoTracking().ToListAsync(cancellationToken);

        return new BanksQueryResult(result);
    }
}
