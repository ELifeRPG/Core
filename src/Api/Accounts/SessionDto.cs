﻿namespace ELifeRPG.Core.Api.Accounts;

public class SessionDto : SessionRequestDto
{
    public Guid AccountId { get; init; }

    public bool Locked { get; init; }
}
