// Used when the player scores.
public sealed class OnScoreUpdated : IEventData
{
    public int Score { get; }
    public OnScoreUpdated(int score)
    {
        Score = score;
    }
}

// Used to request to check and update the highscore after the game is over.
public sealed class OnHighScoreUpdateRequested : IEventData
{
    public int NewScore { get; }
    public OnHighScoreUpdateRequested(int newScore)
    {
        this.NewScore = newScore;
    }
}

// Used when the highscore update request is completed.
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

// Used when the both the score and the highscore are ready to display on the game over panel after high score update request.
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