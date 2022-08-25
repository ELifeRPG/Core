using AutoMapper;
using ELifeRPG.Domain.Banking;

namespace ELifeRPG.Core.Api.Banking;

public class BankProfile : Profile
{
    public BankProfile()
    {
        CreateMap<BankAccount, BankAccountDto>();

        CreateMap<BankAccountTransaction, BankAccountTransactionDto>()
            .ForMember(d => d.BankAccountId, o => o.MapFrom(s => s.BankAccount.Id))
            .ForMember(d => d.BankAccountNumber, o => o.MapFrom(s => s.BankAccount.Number))
            .ForMember(d => d.TargetBankAccountId, o => o.MapFrom(s => s.Target!.Id))
            .ForMember(d => d.TargetBankAccountNumber, o => o.MapFrom(s => s.Target!.Number));
    }
}
