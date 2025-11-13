public sealed class LevelLoadedEvent : IEventData
{
    public int LevelIndex { get; }
    public LevelConfig LevelConfig { get; }
    public LevelLoadedEvent(int levelIndex, LevelConfig levelConfig)
    {
        LevelIndex = levelIndex;
        LevelConfig = levelConfig;
    }
}

public sealed class LevelStartedEvent : IEventData
{
    public int LevelIndex { get; }
    public LevelConfig LevelConfig { get; }
    public LevelStartedEvent(int levelIndex, LevelConfig levelConfig)
    {
        LevelIndex = levelIndex;
        LevelConfig = levelConfig;
    }
}

public sealed class LevelCompletedEvent : IEventData { }

public sealed class AllLevelsCompletedEvent : IEventData { }

public sealed class LevelStopEvent : IEventData { }

public sealed class LevelRestartEvent : IEventData { }