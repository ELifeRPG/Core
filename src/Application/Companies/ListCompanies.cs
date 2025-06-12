using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Companies;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Companies;

public class ListCompaniesResult : AbstractResult
{
    public ListCompaniesResult(List<Company> companies)
    {
        Companies = companies;
    }

    public List<Company> Companies { get; }
}

public class ListCompaniesQuery : IRequest<ListCompaniesResult>
{
}

public class ListCompaniesHandler : IRequestHandler<ListCompaniesQuery, ListCompaniesResult>
{
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public ListCompaniesHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<ListCompaniesResult> Handle(ListCompaniesQuery request, CancellationToken cancellationToken)
    {
        var characters = await _readWriteDatabaseContext.Companies.OrderBy(x => x.Id).ToListAsync(cancellationToken);
        return new ListCompaniesResult(characters);
    }
}
