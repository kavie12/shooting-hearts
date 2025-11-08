public sealed class  OnHighScoreUpdateFailedEvent : EventData
{
    public string Message { get; }

    public OnHighScoreUpdateFailedEvent(string message)
    {
        Message = message;
    }
}