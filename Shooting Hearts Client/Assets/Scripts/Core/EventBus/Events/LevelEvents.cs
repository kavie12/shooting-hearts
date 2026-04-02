// Used when a level is loaded, and level configurations are passed.
public sealed class OnLevelLoaded : IEventData
{
    public LevelConfig LevelConfig { get; }
    public OnLevelLoaded(LevelConfig levelConfig)
    {
        this.LevelConfig = levelConfig;
    }
}

// Used when a level is started, and the level name is passed.
public sealed class OnLevelStarted : IEventData
{
    public string LevelName { get; }
    public OnLevelStarted(string levelName)
    {
        this.LevelName = levelName;
    }
}

// Used when a level is stopped after the spaceship gets destroyed.
public sealed class OnLevelStopped : IEventData { }

// Used when a level is restarted if bonus chance is granted.
public sealed class OnLevelRestarted : IEventData { }

// Used when a level is completed successfully.
public sealed class OnLevelCompleted : IEventData { }

// Used when all levels in the game are completed successfully.
public sealed class OnAllLevelsCompleted : IEventData { }