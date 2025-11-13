public sealed class GameStartEvent : IEventData
{
    public GameConfig GameConfig { get; }
    public GameStartEvent(GameConfig gameConfig)
    {
        this.GameConfig = gameConfig;
    }
}

public sealed class GamePauseEvent : IEventData
{
    public bool IsPaused { get; }
    public GamePauseEvent(bool isPaused) {
        this.IsPaused = isPaused;
    }
}

public sealed class GameOverEvent : IEventData { }