public sealed class GameStartEvent : EventData
{
    public GameConfig GameConfig { get; }

    public GameStartEvent(GameConfig gameConfig)
    {
        this.GameConfig = gameConfig;
    }
}