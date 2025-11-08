using System.Collections.Generic;

public sealed class OnLeaderboardFetchSuccessEvent : EventData
{
    public List<LeaderboardEntry> records { get; }
    public OnLeaderboardFetchSuccessEvent(List<LeaderboardEntry> records)
    {
        this.records = records;
    }
}