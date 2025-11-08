public sealed class UpdateFinalScoreEvent : EventData
{
    public int FinalScore { get; }

    public UpdateFinalScoreEvent(int finalScore)
    {
        this.FinalScore = finalScore;
    }
}