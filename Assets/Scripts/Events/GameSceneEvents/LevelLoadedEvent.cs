public sealed class LevelLoadedEvent : EventData
{
    public int LevelIndex { get; }
    public LevelConfig LevelConfig { get; }
    public LevelLoadedEvent(int levelIndex, LevelConfig levelConfig)
    {
        LevelIndex = levelIndex;
        LevelConfig = levelConfig;
    }
}