using System.Collections.Generic;

// Used to request high score of the player.
public sealed class OnHighScoreRequest : IEventData { }

// Used when a player high score request is completed.
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

// Used to request leaderboard records.
public sealed class OnLeaderboardRequest : IEventData { }

// Used when a leaderboard request is completed.
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