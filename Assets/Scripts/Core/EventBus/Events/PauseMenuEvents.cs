public sealed class PauseMenuToggleEvent : IEventData
{
    public bool Paused { get; }
    public PauseMenuToggleEvent(bool paused)
    {
        this.Paused = paused;
    }
}
public sealed class PauseMenuResumeButtonClickEvent : IEventData { }
public sealed class PauseMenuMainMenuButtonClickEvent : IEventData { }
public sealed class PauseMenuQuitGameButtonClickEvent : IEventData { }