// Used when the game is started.
public sealed class OnGameStarted : IEventData
{
    public GameConfig GameConfig { get; }
    public OnGameStarted(GameConfig gameConfig)
    {
        this.GameConfig = gameConfig;
    }
}

// Used when the game is stopped due to the spaceship destruction.
public sealed class OnGameStopped : IEventData { }

// Used when the game is continued after receiving the bonus chance.
public sealed class OnGameContinued : IEventData { }

// Used when the game is over, indicating whether the player has won or lost.
public sealed class OnGameOver : IEventData
{
    public bool Win { get; }
    public OnGameOver(bool win)
    {
        Win = win;
    }
}