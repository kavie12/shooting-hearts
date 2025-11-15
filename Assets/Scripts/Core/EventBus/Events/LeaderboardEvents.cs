using System.Collections.Generic;

public sealed class OnLeaderboardRequest : IEventData { }

public sealed class OnLeaderboardRequestCompleted : IEventData
{
    public bool Success { get; }
    public List<LeaderboardEntry> Records { get; }
    public OnLeaderboardRequestCompleted(bool success, List<LeaderboardEntry> records)
    {
        this.Success = success;
        this.Records = records;
    }
}