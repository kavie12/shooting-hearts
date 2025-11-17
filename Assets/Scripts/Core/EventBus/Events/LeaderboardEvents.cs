using System.Collections.Generic;

public sealed class OnHighScoreRequest : IEventData { }

public sealed class OnHighScoreRequestCompleted : IEventData
{
    public bool Success { get; }
    public string PlayerName { get; }
    public int HighScore { get; }
    public OnHighScoreRequestCompleted(bool success, string playerName, int highScore)
    {
        Success = success;
        PlayerName = playerName;
        HighScore = highScore;
    }
}

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