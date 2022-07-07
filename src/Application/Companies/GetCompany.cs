using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Companies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Companies;

public class GetCompanyResult : ResultBase
{
    public GetCompanyResult(Company company, ICollection<CompanyMembership> topMembers)
    {
        Company = company;
        TopMembers = topMembers;
    }
    
    public Company Company { get; }
    
    public ICollection<CompanyMembership> TopMembers { get; }
}

public class GetCompanyQuery : IRequest<GetCompanyResult>
{
    public GetCompanyQuery(Guid companyId)
    {
        CompanyId = companyId;
    }
    
    public Guid CompanyId { get; }
}

public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, GetCompanyResult>
{
    private readonly IDatabaseContext _databaseContext;

    public GetCompanyHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<GetCompanyResult> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _databaseContext.Companies
            .Include(x => x.Memberships!.OrderBy(m => m.Position.Ordering).Take(5))
            .SingleOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);
        
        if (company is null)
        {
            throw new ELifeEntityNotFoundException($"Could not find company with Id {request.CompanyId}.");
        }

        return new GetCompanyResult(company, company.Memberships!);
    }
}
