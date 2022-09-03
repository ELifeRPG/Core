using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ELifeRPG.Application.Banking.Commands;
using ELifeRPG.Application.Banking.Queries;
using ELifeRPG.Core.Api.Banking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class BankingEndpoints
{
    private const string Tag = "Banking";

    public static WebApplication MapBankingEndpoints(this WebApplication app)
    {
        app
            .MapGet(
                "/banks",
                async (IMediator mediator, CancellationToken cancellationToken)
                    => Results.Ok((await mediator.Send(new BanksQuery(), cancellationToken)).Banks))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Lists all banks.");

        app
            .MapPost(
                "/banks/{bankId:guid}/accounts",
                async ([FromRoute] Guid bankId, [FromQuery] Guid characterId, IMediator mediator, IMapper mapper)
                    => Results.Ok(mapper.Map<BankAccountDto>(
                        (await mediator.Send(new OpenBankAccountCommand(bankId, characterId))).BankAccount)))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Creates a new bank account.");

        app
            .MapGet(
                "/characters/{characterId:guid}/bank-accounts",
                async (Guid characterId, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<List<BankAccountDto>>(
                        (await mediator.Send(new BankAccountsQuery { CharacterId = characterId }, cancellationToken))
                        .BankAccounts)))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Lists bank-accounts of the given character.");

        app
            .MapPut(
                "/bank-accounts/{bankAccountId:guid}/transactions",
                async ([FromRoute] Guid bankAccountId, [FromQuery] Guid characterId, [FromBody] BankAccountTransactionDto transaction, IMediator mediator, IMapper mapper)
                    => Results.Ok(
                        mapper.Map<BankAccountTransactionDto>(
                            (await mediator.Send(
                                new MakeTransactionCommand
                                {
                                    SourceBankAccountId = bankAccountId,
                                    TargetBankAccountId = transaction.TargetBankAccountId,
                                    CharacterId = characterId,
                                    Amount = transaction.Amount,
                                })
                            ).Transaction)))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Performs a transaction to a bank-account.");

        app
            .MapPut(
                "/bank-accounts/{bankAccountId:guid}/withdraw",
                async ([FromRoute] Guid bankAccountId, [FromQuery] [Required] Guid characterId, [FromBody] BankAccountWithdrawalCommandDto withdrawal, IMediator mediator, IMapper mapper)
                    => Results.Ok(
                        mapper.Map<BankAccountWithdrawalResultDto>(
                            (await mediator.Send(
                                new WithdrawMoneyCommand
                                {
                                    BankAccountId = bankAccountId,
                                    CharacterId = characterId,
                                    Amount = withdrawal.Amount,
                                })
                            ).Transaction)))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Withdraws money from a bank-account.");

        app
            .MapPut(
                "/bank-accounts/{bankAccountId:guid}/deposit",
                async ([FromRoute] Guid bankAccountId, [FromBody] BankAccountDepositCommandDto deposit, IMediator mediator, IMapper mapper)
                    => Results.Ok(
                        mapper.Map<BankAccountDepositResultDto>(
                            (await mediator.Send(
                                new DepositMoneyCommand
                                {
                                    BankAccountId = bankAccountId,
                                    Amount = deposit.Amount,
                                })
                            ))))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Deposits money to a bank-account.");

        return app;
    }
}
