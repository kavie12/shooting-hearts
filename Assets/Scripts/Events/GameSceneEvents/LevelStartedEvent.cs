public sealed class LevelStartedEvent : EventData
{
    public int LevelIndex { get; }
    public LevelConfig LevelConfig { get; }

    public LevelStartedEvent(int levelIndex, LevelConfig levelConfig)
    {
        LevelIndex = levelIndex;
        LevelConfig = levelConfig;
    }
}