public sealed class OnScoreUpdated : IEventData
{
    public int Score { get; }
    public OnScoreUpdated(int score)
    {
        Score = score;
    }
}

public sealed class OnHighScoreUpdateRequested : IEventData
{
    public int NewScore { get; }
    public OnHighScoreUpdateRequested(int newScore)
    {
        this.NewScore = newScore;
    }
}

public sealed class OnHighScoreUpdateRequestCompleted : IEventData
{
    public bool Success { get; }
    public int HighScore { get; }
    public OnHighScoreUpdateRequestCompleted(bool success, int highScore)
    {
        Success = success;
        HighScore = highScore;
    }
}

public sealed class OnUpdatedHighScoreFetched : IEventData
{
    public int Score { get; }
    public int HighScore { get; }
    public OnUpdatedHighScoreFetched(int score, int highScore)
    {
        Score = score;
        HighScore = highScore;
    }
}