using ELifeRPG.Domain.Characters;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ELifeRPG.Application.Characters;

public class CharacterLogManager : INotificationHandler<CharacterCreatedEvent>
{
    private readonly ILogger<CharacterLogManager> _logger;

    public CharacterLogManager(ILogger<CharacterLogManager> logger)
    {
        _logger = logger;
    }

    public Task Handle(CharacterCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Character {CharacterId} has been created", notification.Character.Id);
        return Task.CompletedTask;
    }
}
