namespace ELifeRPG.Core.WebUI.Shared;

public interface ISettingsStore
{
    event Func<bool, Task>? DarkModeChanged;
    
    Task<bool> GetDarkMode();

    Task SetDarkMode(bool active);

    Task ToggleDarkMode();
}

public class SettingsStore : ISettingsStore
{
    private bool? _darkModeIsActive;

    public event Func<bool, Task>? DarkModeChanged;
    
    public Task<bool> GetDarkMode()
    {
        return Task.FromResult(_darkModeIsActive ?? false);
    }

    public async Task SetDarkMode(bool active)
    {
        _darkModeIsActive = active;
        
        if (DarkModeChanged is not null)
        {
            await DarkModeChanged.Invoke(active);
        }
    }
    
    public async Task ToggleDarkMode()
    {
        var newValue = !(_darkModeIsActive ?? false);
        _darkModeIsActive = newValue;
        
        if (DarkModeChanged is not null)
        {
            await DarkModeChanged.Invoke(newValue);
        }
    }
}
