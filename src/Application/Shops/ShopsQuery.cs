using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Shops;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Shops;

public class ShopsQueryResult : AbstractResult
{
    public ShopsQueryResult(ICollection<Shop> shops)
    {
        Shops = shops;
    }
    
    public ICollection<Shop> Shops { get; }
}

public class ShopsQuery : IRequest<ShopsQueryResult>
{
}

internal class ShopsQueryHandler : IRequestHandler<ShopsQuery, ShopsQueryResult>
{
    private readonly IDatabaseContext _databaseContext;

    public ShopsQueryHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ShopsQueryResult> Handle(ShopsQuery request, CancellationToken cancellationToken)
    {
        return new ShopsQueryResult(await _databaseContext.Shops.ToListAsync(cancellationToken));
    }
}
