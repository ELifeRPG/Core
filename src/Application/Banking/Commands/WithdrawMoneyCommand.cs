using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Banking;
using Mediator;
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
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public WithdrawMoneyCommandHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<WithdrawMoneyCommandResult> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
    {
        var bankAccount = await _readWriteDatabaseContext.BankAccounts
            .Include(x => x.Owner.Character)
            .Include(x => x.BankCondition)
            .Include(x => x.Bookings!.Take(0))
            .SingleOrDefaultAsync(x => x.Id == request.BankAccountId, cancellationToken);

        if (bankAccount is null)
        {
            throw new ELifeEntityNotFoundException();
        }

        var character = await _readWriteDatabaseContext.Characters
            .SingleOrDefaultAsync(x => x.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            throw new ELifeEntityNotFoundException();
        }

        var transaction = bankAccount.WithdrawMoney(character, request.Amount);

        await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);

        return new WithdrawMoneyCommandResult(transaction);
    }
}
