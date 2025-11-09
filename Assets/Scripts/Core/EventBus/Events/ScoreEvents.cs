public sealed class ScoreUpdatedEvent : IEventData
{
    public int Score { get; }
    public ScoreUpdatedEvent(int score)
    {
        Score = score;
    }
}

public sealed class UpdateFinalScoreEvent : IEventData
{
    public int FinalScore { get; }
    public UpdateFinalScoreEvent(int finalScore)
    {
        this.FinalScore = finalScore;
    }
}

public sealed class OnFinalScoresReadyEvent : IEventData
{
    public int Score { get; }
    public int HighScore { get; }
    public OnFinalScoresReadyEvent(int score, int highScore)
    {
        Score = score;
        HighScore = highScore;
    }
}