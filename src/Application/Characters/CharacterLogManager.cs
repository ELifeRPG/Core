using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Characters.Sessions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ELifeRPG.Application.Characters;

public class CharacterLogManager : INotificationHandler<CharacterCreatedEvent>, INotificationHandler<CharacterSessionCreatedEvent>
{
    private readonly ILogger<CharacterLogManager> _logger;

    public CharacterLogManager(ILogger<CharacterLogManager> logger)
    {
        _logger = logger;
    }

    public Task Handle(CharacterCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Character {CharacterId} has been created", notification.Character.Id.ToString());
        return Task.CompletedTask;
    }
    
    public Task Handle(CharacterSessionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Session for character {CharacterId} has been started", notification.Character.Id.ToString());
        return Task.CompletedTask;
    }
}
