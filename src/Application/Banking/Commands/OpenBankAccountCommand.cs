﻿using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common.Exceptions;
using ELifeRPG.Domain.Companies;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Banking.Commands;

public class OpenBankAccountCommandResult : AbstractResult
{
    public OpenBankAccountCommandResult(BankAccount bankAccount)
    {
        BankAccount = bankAccount;
    }

    public BankAccount BankAccount { get; }
}

public class OpenBankAccountCommand : IRequest<OpenBankAccountCommandResult>
{
    public OpenBankAccountCommand(Guid bankId, Guid characterId)
    {
        BankId = bankId;
        CharacterId = characterId;
    }

    public OpenBankAccountCommand(Guid bankId, CompanyId companyId)
    {
        BankId = bankId;
        CompanyId = companyId;
    }

    public Guid BankId { get; }

    public Guid? CharacterId { get; }

    public CompanyId? CompanyId { get; }
}

internal class OpenBankAccountCommandHandler : IRequestHandler<OpenBankAccountCommand, OpenBankAccountCommandResult>
{
    private readonly IReadWriteDatabaseContext _readWriteDatabaseContext;

    public OpenBankAccountCommandHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    {
        _readWriteDatabaseContext = readWriteDatabaseContext;
    }

    public async ValueTask<OpenBankAccountCommandResult> Handle(OpenBankAccountCommand request, CancellationToken cancellationToken)
    {
        if (request.CharacterId is null && request.CompanyId is null)
        {
            throw new ELifeInvalidOperationException();
        }

        var bank = await _readWriteDatabaseContext.Banks
            .Include(x => x.Country)
            .Include(x => x.Accounts)
            .Include(x => x.Conditions)
            .SingleOrDefaultAsync(x => x.Id == request.BankId, cancellationToken);

        if (bank is null)
        {
            throw new ELifeEntityNotFoundException();
        }

        var bankAccount = request.CompanyId is null
            ? await OpenAccountForCharacter(bank, request.CharacterId!.Value, cancellationToken)
            : await OpenAccountForCompany(bank, request.CompanyId.Value, cancellationToken);

        await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);

        return new OpenBankAccountCommandResult(bankAccount);
    }

    private async Task<BankAccount> OpenAccountForCharacter(Bank bank, Guid characterId, CancellationToken cancellationToken)
    {
        var character = await _readWriteDatabaseContext.Characters
            .Include(x => x.Person)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == characterId, cancellationToken);

        if (character is null)
        {
            throw new ELifeEntityNotFoundException();
        }

        var bankAccount = bank.OpenAccount(character.Person!);
        await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);

        return bankAccount;
    }

    private async Task<BankAccount> OpenAccountForCompany(Bank bank, CompanyId companyId, CancellationToken cancellationToken)
    {
        var company = await _readWriteDatabaseContext.Companies
            .Include(x => x.Person)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == companyId.Value, cancellationToken);

        if (company is null)
        {
            throw new ELifeEntityNotFoundException();
        }

        var bankAccount = bank.OpenAccount(company.Person!);
        await _readWriteDatabaseContext.SaveChangesAsync(cancellationToken);

        return bankAccount;
    }
}
