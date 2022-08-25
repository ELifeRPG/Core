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
                async ([FromRoute]Guid bankId, [FromQuery] Guid characterId, IMediator mediator, IMapper mapper)
                    => Results.Ok(mapper.Map<BankAccountDto>((await mediator.Send(new OpenBankAccountCommand(bankId, characterId))).BankAccount)))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Creates a new bank account.");
        
        app
            .MapGet(
                "/characters/{characterId:guid}/bank-accounts",
                async (Guid characterId, IMediator mediator, IMapper mapper, CancellationToken cancellationToken) 
                    => Results.Ok(mapper.Map<List<BankAccountDto>>((await mediator.Send(new BankAccountsQuery { CharacterId = characterId }, cancellationToken)).BankAccounts)))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Lists bank-accounts of the given character.");
        
        app
            .MapPut(
                "/bank-accounts/{bankAccountId:guid}/transaction",
                async ([FromRoute]Guid bankAccountId, [FromQuery] Guid characterId, [FromBody] BankAccountTransactionDto transactionDto, IMediator mediator, IMapper mapper)
                    => Results.Ok(
                        mapper.Map<BankAccountTransactionDto>(
                            (await mediator.Send(
                                new MakeTransactionCommand 
                                {
                                    SourceBankAccountId = bankAccountId,
                                    TargetBankAccountId = transactionDto.TargetBankAccountId!.Value,
                                    CharacterId = characterId,
                                    Amount = transactionDto.Amount,
                                })
                            ).Transaction)))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Performs a transaction.");
        
        app
            .MapPut(
                "/bank-accounts/{bankAccountId:guid}/withdraw",
                async ([FromRoute]Guid bankAccountId, [FromQuery] Guid characterId, [FromBody] BankAccountTransactionDto transactionDto, IMediator mediator, IMapper mapper)
                    => Results.Ok(mapper.Map<BankAccountTransactionDto>((await mediator.Send(new MakeTransactionCommand {  })).Transaction)))
            .Produces<string>()
            .WithTags(Tag)
            .WithSummary("Withdraws money from a bank-account.");

        return app;
    }
}
