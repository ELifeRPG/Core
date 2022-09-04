using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Banking;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Banking.Commands;

public class WithdrawMoneyCommandResult : AbstractResult
{
    public WithdrawMoneyCommandResult(BankAccountTransaction transaction)
    {
        Transaction = transaction;
    }
    
    public BankAccountTransaction Transaction { get; }
}

public class WithdrawMoneyCommand : IRequest<WithdrawMoneyCommandResult>
{
    public Guid BankAccountId { get; init; }
    
    public Guid CharacterId { get; init; }
    
    public decimal Amount { get; init; }
}

internal class WithdrawMoneyCommandHandler : IRequestHandler<WithdrawMoneyCommand, WithdrawMoneyCommandResult>
{
    private readonly IDatabaseContext _databaseContext;

    public WithdrawMoneyCommandHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<WithdrawMoneyCommandResult> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
    {
        var bankAccount = await _databaseContext.BankAccounts
            .Include(x => x.OwningCharacter)
            .Include(x => x.BankCondition)
            .SingleOrDefaultAsync(x => x.Id == request.BankAccountId, cancellationToken);

        if (bankAccount is null)
        {
            throw new ELifeEntityNotFoundException();
        }
        
        var character = await _databaseContext.Characters
            .SingleOrDefaultAsync(x => x.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            throw new ELifeEntityNotFoundException();
        }
        
        var transaction = bankAccount.WithdrawMoney(character, request.Amount);

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new WithdrawMoneyCommandResult(transaction);
    }
}
