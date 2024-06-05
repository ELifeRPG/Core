using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Banking;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Banking.Commands;

public class DepositMoneyCommandResult : AbstractResult
{
    public DepositMoneyCommandResult(BankAccountTransaction transaction, BankAccountBooking booking)
    {
        Transaction = transaction;
        Booking = booking;
    }
    
    public BankAccountTransaction Transaction { get; }

    public BankAccountBooking Booking { get; }
}

public class DepositMoneyCommand : IRequest<DepositMoneyCommandResult>
{
    public Guid BankAccountId { get; init; }
    
    public decimal Amount { get; init; }
}

internal class DepositMoneyCommandHandler : IRequestHandler<DepositMoneyCommand, DepositMoneyCommandResult>
{
    private readonly IDatabaseContext _databaseContext;

    public DepositMoneyCommandHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<DepositMoneyCommandResult> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
    {
        var bankAccount = await _databaseContext.BankAccounts
            .Include(x => x.Owner.Character)
            .Include(x => x.Owner.Company)
            .Include(x => x.BankCondition)
            .Include(x => x.Bookings!.Take(0))
            .SingleOrDefaultAsync(x => x.Id == request.BankAccountId, cancellationToken);

        if (bankAccount is null)
        {
            throw new ELifeEntityNotFoundException();
        }
        
        var (transaction, booking) = bankAccount.DepositMoney(request.Amount);

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new DepositMoneyCommandResult(transaction, booking);
    }
}
