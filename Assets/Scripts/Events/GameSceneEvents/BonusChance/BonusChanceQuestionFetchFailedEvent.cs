public sealed class BonusChanceQuestionFetchFailedEvent : EventData
{
    public string Message { get; }

    public BonusChanceQuestionFetchFailedEvent(string message)
    {
        Message = message;
    }
}