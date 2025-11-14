public sealed class GameStartEvent : IEventData
{
    public GameConfig GameConfig { get; }
    public GameStartEvent(GameConfig gameConfig)
    {
        this.GameConfig = gameConfig;
    }
}

public sealed class GamePauseEvent : IEventData { }

public sealed class GameResumeEvent : IEventData { }

public sealed class GameOverEvent : IEventData { }

public sealed class BackToMainMenuEvent : IEventData { }