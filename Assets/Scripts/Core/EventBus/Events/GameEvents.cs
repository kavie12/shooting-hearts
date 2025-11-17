public sealed class OnGameStarted : IEventData
{
    public GameConfig GameConfig { get; }
    public OnGameStarted(GameConfig gameConfig)
    {
        this.GameConfig = gameConfig;
    }
}

public sealed class OnGameStopped : IEventData { }

public sealed class OnGameContinued : IEventData { }

public sealed class OnGameOver : IEventData
{
    public bool Win { get; }
    public OnGameOver(bool win)
    {
        Win = win;
    }
}