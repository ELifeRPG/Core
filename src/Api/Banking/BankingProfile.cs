using AutoMapper;
using ELifeRPG.Application.Banking.Commands;
using ELifeRPG.Domain.Banking;

namespace ELifeRPG.Core.Api.Banking;

public class BankingProfile : Profile
{
    public BankingProfile()
    {
        CreateMap<BankAccount, BankAccountDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Number, o => o.MapFrom(s => new BankAccountNumber(s.Number).ToString()))
            .ForMember(d => d.Balance, o => o.MapFrom(s => s.Balance))
            .ForMember(d => d.Bookings, o => o.MapFrom(s => s.Bookings));

        CreateMap<BankAccountBooking, BankAccountBookingDto>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
            .ForMember(d => d.Source, o => o.MapFrom(s => s.Source))
            .ForMember(d => d.Purpose, o => o.MapFrom(s => s.Purpose))
            .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount));
        
        CreateMap<BankAccountTransaction, BankAccountTransactionDto>()
            .ForMember(d => d.BankAccountId, o => o.MapFrom(s => s.BankAccount.Id))
            .ForMember(d => d.BankAccountNumber, o => o.MapFrom(s => s.BankAccount.Number))
            .ForMember(d => d.TargetBankAccountId, o => o.MapFrom(s => s.Source!.Id))
            .ForMember(d => d.TargetBankAccountNumber, o => o.MapFrom(s => s.Source!.Number))
            .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
            .ForMember(d => d.Fees, o => o.MapFrom(s => s.Fees));
        
        CreateMap<BankAccountTransaction, BankAccountWithdrawalResultDto>()
            .ForMember(d => d.BankAccountId, o => o.MapFrom(s => s.BankAccount.Id))
            .ForMember(d => d.BankAccountNumber, o => o.MapFrom(s => s.BankAccount.Number))
            .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
            .ForMember(d => d.Fees, o => o.MapFrom(s => s.Fees));
        
        CreateMap<DepositMoneyCommandResult, BankAccountDepositResultDto>()
            .ForMember(d => d.Amount, o => o.MapFrom(s => s.Booking.Amount))
            .ForMember(d => d.Fees, o => o.MapFrom(s => s.Transaction.Fees));
    }
}
