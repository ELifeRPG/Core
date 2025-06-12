using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Banking;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Banking.Commands;

public class MakeTransactionCommandResult : AbstractResult
{
    public MakeTransactionCommandResult(BankAccountTransaction transaction)
    {
        Transaction = transaction;
    }

    public BankAccountTransaction Transaction { get; }
}

public class MakeTransactionCommand : IRequest<MakeTransactionCommandResult>
{
    public Guid SourceBankAccountId { get; init; }

    public Guid TargetBankAccountId { get; init; }

    public Guid CharacterId { get; init; }

    public decimal Amount { get; init; }
}

internal class MakeTransactionCommandHandler : IRequestHandler<MakeTransactionCommand, MakeTransactionCommandResult>
{
    private readonly IDatabaseContext _databaseContext;

    public MakeTransactionCommandHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async ValueTask<MakeTransactionCommandResult> Handle(MakeTransactionCommand request, CancellationToken cancellationToken)
    {
        var selectedBankAccounts = await _databaseContext.BankAccounts
            .Include(x => x.Bookings)
            .Include(x => x.BankCondition)
            .Where(x => x.Id == request.SourceBankAccountId || x.Id == request.TargetBankAccountId)
            .ToListAsync(cancellationToken);

        if (selectedBankAccounts.Count < 2)
        {
            throw new ELifeEntityNotFoundException();
        }

        var character = await _databaseContext.Characters
            .SingleOrDefaultAsync(x => x.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            throw new ELifeEntityNotFoundException();
        }

        var sourceBankAccount = selectedBankAccounts.First(x => x.Id == request.SourceBankAccountId);
        var targetBankAccount = selectedBankAccounts.First(x => x.Id == request.TargetBankAccountId);

        var transaction = sourceBankAccount.TransferMoneyTo(targetBankAccount, request.Amount, character);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new MakeTransactionCommandResult(transaction);
    }
}
