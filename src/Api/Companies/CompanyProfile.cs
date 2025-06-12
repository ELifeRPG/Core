using AutoMapper;
using ELifeRPG.Application.Companies;
using ELifeRPG.Core.Api.Models;
using ELifeRPG.Domain.Companies;

namespace ELifeRPG.Core.Api.Companies;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ReverseMap();

        CreateMap<ListCompaniesResult, ResultDto<List<CompanyDto>>>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s.Companies));
    }
}
