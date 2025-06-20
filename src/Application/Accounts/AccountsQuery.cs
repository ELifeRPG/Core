﻿using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace ELifeRPG.Application.Accounts;

[GenerateOneOf]
public partial class AccountsQueryResponse : OneOfBase<List<Account>>;

public class AccountsQuery : IRequest<AccountsQueryResponse>;

public class ListAccountsHandler(IReadWriteDatabaseContext readWriteDatabaseContext)
    : IRequestHandler<AccountsQuery, AccountsQueryResponse>
{
    public async ValueTask<AccountsQueryResponse> Handle(AccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await readWriteDatabaseContext.Accounts
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new List<Account>(accounts);
    }
}
