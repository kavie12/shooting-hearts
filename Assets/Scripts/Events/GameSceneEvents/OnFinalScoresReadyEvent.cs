public sealed class OnFinalScoresReadyEvent : EventData
{
    public int Score { get; }
    public int HighScore { get; }

    public OnFinalScoresReadyEvent(int score, int highScore)
    {
        Score = score;
        HighScore = highScore;
    }
}