public sealed class OnLeaderboardFetchFailedEvent : EventData
{
    public string Message { get; }
    public OnLeaderboardFetchFailedEvent(string message)
    {
        Message = message;
    }
}