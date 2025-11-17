public sealed class OnGamePaused : IEventData
{
    public bool Paused { get; }
    public OnGamePaused(bool paused)
    {
        Paused = paused;
    }
}

public sealed class OnPauseMenuButtonClicked : IEventData
{
    public PauseMenuButton ButtonId { get; }
    public OnPauseMenuButtonClicked(PauseMenuButton buttonId)
    {
        ButtonId = buttonId;
    }
}