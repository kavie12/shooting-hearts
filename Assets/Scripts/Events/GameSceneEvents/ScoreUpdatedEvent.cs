public sealed class ScoreUpdatedEvent : EventData
{
    public int Score { get; }

    public ScoreUpdatedEvent(int score)
    {
        Score = score;
    }
}