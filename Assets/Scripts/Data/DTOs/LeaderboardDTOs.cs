using System.Collections.Generic;

[System.Serializable]
public class LeaderboardResponse
{
    public List<LeaderboardResponseRecord> leaderboard;
}

[System.Serializable]
public class LeaderboardResponseRecord
{
    public string playerName;
    public int playerScore;
}

[System.Serializable]
public class UpdateHighScoreRequest
{
    public int newScore;
}

[System.Serializable]
public class UpdateHighScoreResponse
{
    public int highScore;
}