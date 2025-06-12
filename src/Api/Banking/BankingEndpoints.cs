using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ELifeRPG.Application.Banking.Commands;
using ELifeRPG.Application.Banking.Queries;
using ELifeRPG.Core.Api.Banking;
using Mediator;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class BankingEndpoints
{
    public const string Tag = "Banking";

    public static WebApplication MapBankingEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("v1")
            .WithGroupName("v1")
            .WithTags(Tag);

        group
            .MapGet(
                "banks",
                async (IMediator mediator, CancellationToken cancellationToken)
                    => Results.Ok((await mediator.Send(new BanksQuery(), cancellationToken)).Banks))
            .Produces<string>()
            .WithSummary("Lists all banks.");

        group
            .MapPost(
                "banks/{bankId:guid}/accounts",
                async ([FromRoute] Guid bankId, [FromQuery] Guid characterId, IMediator mediator, IMapper mapper)
                    => Results.Ok(mapper.Map<BankAccountDto>(
                        (await mediator.Send(new OpenBankAccountCommand(bankId, characterId))).BankAccount)))
            .Produces<string>()
            .WithSummary("Creates a new bank account.");

        group
            .MapGet(
                "characters/{characterId:guid}/bank-accounts",
                async (Guid characterId, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<List<BankAccountDto>>(
                        (await mediator.Send(new BankAccountsQuery { CharacterId = characterId }, cancellationToken))
                        .BankAccounts)))
            .Produces<string>()
            .WithSummary("Lists bank-accounts of the given character.");

        group
            .MapGet(
                "bank-accounts/{bankAccountId:guid}",
                async (Guid bankAccountId, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<BankAccountDto>(
                        (await mediator.Send(new BankAccountDetailsQuery(bankAccountId), cancellationToken))
                        .BankAccount)))
            .Produces<string>()
            .WithSummary("Gets bank-account with details.");

        group
            .MapPut(
                "bank-accounts/{bankAccountId:guid}/transaction",
                async ([FromRoute] Guid bankAccountId, [FromQuery] Guid characterId, [FromBody] BankAccountTransactionCommandDto transaction, IMediator mediator, IMapper mapper)
                    => Results.Ok(
                        mapper.Map<BankAccountTransactionDto>(
                            (await mediator.Send(
                                new MakeTransactionCommand
                                {
                                    SourceBankAccountId = bankAccountId,
                                    TargetBankAccountId = transaction.TargetBankAccountId,
                                    CharacterId = characterId,
                                    Amount = transaction.Amount,
                                }))
                            .Transaction)))
            .Produces<string>()
            .WithSummary("Performs a transaction to a bank-account.");

        group
            .MapPut(
                "bank-accounts/{bankAccountId:guid}/withdraw",
                async ([FromRoute] Guid bankAccountId, [FromQuery] [Required] Guid characterId, [FromBody] BankAccountWithdrawalCommandDto withdrawal, IMediator mediator, IMapper mapper)
                    => Results.Ok(
                        mapper.Map<BankAccountWithdrawalResultDto>(
                            (await mediator.Send(
                                new WithdrawMoneyCommand
                                {
                                    BankAccountId = bankAccountId,
                                    CharacterId = characterId,
                                    Amount = withdrawal.Amount,
                                }))
                            .Transaction)))
            .Produces<string>()
            .WithSummary("Withdraws money from a bank-account.");

        group
            .MapPut(
                "bank-accounts/{bankAccountId:guid}/deposit",
                async ([FromRoute] Guid bankAccountId, [FromBody] BankAccountDepositCommandDto deposit, IMediator mediator, IMapper mapper)
                    => Results.Ok(
                        mapper.Map<BankAccountDepositResultDto>(
                            await mediator.Send(
                                new DepositMoneyCommand
                                {
                                    BankAccountId = bankAccountId,
                                    Amount = deposit.Amount,
                                }))))
            .Produces<string>()
            .WithSummary("Deposits money to a bank-account.");

        return app;
    }
}
