// Used when the game is paused or unpaused.
public sealed class OnGamePaused : IEventData
{
    public bool Paused { get; }
    public OnGamePaused(bool paused)
    {
        Paused = paused;
    }
}

// Used when a button in the pause menu is clicked, indicating which button in the PauseMenuButton enum is clicked.
public sealed class OnPauseMenuButtonClicked : IEventData
{
    public PauseMenuButton ButtonId { get; }
    public OnPauseMenuButtonClicked(PauseMenuButton buttonId)
    {
        ButtonId = buttonId;
    }
}