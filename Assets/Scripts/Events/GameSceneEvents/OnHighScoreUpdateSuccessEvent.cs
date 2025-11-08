public sealed class  OnHighScoreUpdateSuccessEvent : EventData
{
    public int HighScore { get; }

    public OnHighScoreUpdateSuccessEvent(int highScore)
    {
        HighScore = highScore;
    }
}