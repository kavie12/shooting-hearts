public sealed class LevelLoadedEvent : EventData
{
    public LevelConfig LevelConfig { get; }
    public LevelLoadedEvent(LevelConfig levelConfig)
    {
        LevelConfig = levelConfig;
    }
}