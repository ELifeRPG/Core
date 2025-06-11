using ELifeRPG.Domain.Characters.Events;
using ELifeRPG.Domain.Characters.Sessions;
using Mediator;
using Microsoft.Extensions.Logging;

namespace ELifeRPG.Application.Characters;

public class CharacterLogManager : INotificationHandler<CharacterCreatedEvent>, INotificationHandler<CharacterSessionCreatedEvent>
{
    private readonly ILogger<CharacterLogManager> _logger;

    public CharacterLogManager(ILogger<CharacterLogManager> logger)
    {
        _logger = logger;
    }

    public ValueTask Handle(CharacterCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Character {CharacterId} has been created", notification.Character.Id.ToString());
        return ValueTask.CompletedTask;
    }

    public ValueTask Handle(CharacterSessionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Session for character {CharacterId} has been started", notification.Character.Id.ToString());
        return ValueTask.CompletedTask;
    }
}
