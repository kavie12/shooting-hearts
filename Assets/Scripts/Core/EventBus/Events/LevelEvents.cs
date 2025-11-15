public sealed class OnLevelLoaded : IEventData
{
    public LevelConfig LevelConfig { get; }
    public OnLevelLoaded(LevelConfig levelConfig)
    {
        this.LevelConfig = levelConfig;
    }
}

public sealed class OnLevelStarted : IEventData
{
    public string LevelName { get; }
    public OnLevelStarted(string levelName)
    {
        this.LevelName = levelName;
    }
}

public sealed class OnLevelStopped : IEventData { }

public sealed class OnLevelRestarted : IEventData { }

public sealed class OnLevelCompleted : IEventData { }

public sealed class OnAllLevelsCompleted : IEventData { }