public sealed class GameStartEvent : IEventData
{
    public GameConfig GameConfig { get; }
    public GameStartEvent(GameConfig gameConfig)
    {
        this.GameConfig = gameConfig;
    }
}

public sealed class GameStopEvent : IEventData
{
}

public sealed class GameContinueEvent : IEventData
{
}

public sealed class GameOverEvent : IEventData
{
}