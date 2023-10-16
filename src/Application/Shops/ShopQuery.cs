using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Shops;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Shops;

public class ShopQueryResult : AbstractResult
{
    public required Shop Shop { get; init; }
}

public class ShopQuery : IRequest<ShopQueryResult>
{
    public required Guid ShopId { get; init; }
}

internal class ShopQueryHandler : IRequestHandler<ShopQuery, ShopQueryResult>
{
    private readonly IDatabaseContext _databaseContext;

    public ShopQueryHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ShopQueryResult> Handle(ShopQuery request, CancellationToken cancellationToken)
    {
        var shop = await _databaseContext.Shops
            .Include(x => x.Listings)!.ThenInclude(x => x.Item)
            .SingleOrDefaultAsync(x => x.Id == request.ShopId, cancellationToken);

        if (shop is null)
        {
            throw new ELifeEntityNotFoundException();
        }
        
        return new ShopQueryResult
        {
            Shop = shop,
        };
    }
}
