using System.Collections.Generic;

public sealed class OnLeaderboardFetchSuccessEvent : IEventData
{
    public List<LeaderboardEntry> Records { get; }
    public OnLeaderboardFetchSuccessEvent(List<LeaderboardEntry> records)
    {
        this.Records = records;
    }
}

public sealed class OnLeaderboardFetchFailedEvent : IEventData
{
    public string Message { get; }
    public OnLeaderboardFetchFailedEvent(string message)
    {
        Message = message;
    }
}

public sealed class OnHighScoreUpdateSuccessEvent : IEventData
{
    public int HighScore { get; }
    public OnHighScoreUpdateSuccessEvent(int highScore)
    {
        HighScore = highScore;
    }
}

public sealed class OnHighScoreUpdateFailedEvent : IEventData
{
    public string Message { get; }
    public OnHighScoreUpdateFailedEvent(string message)
    {
        Message = message;
    }
}