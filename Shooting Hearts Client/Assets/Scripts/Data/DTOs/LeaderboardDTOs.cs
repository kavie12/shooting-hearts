using System.Collections.Generic;

[System.Serializable]
public class PlayerHighScoreResponse
{
    public string playerName;
    public int highScore;
}

[System.Serializable]
public class LeaderboardResponse
{
    public List<LeaderboardResponseRecord> leaderboard;
}

[System.Serializable]
public class LeaderboardErrorResponse
{
    public string message;
}

[System.Serializable]
public class LeaderboardResponseRecord
{
    public string playerName;
    public int playerScore;
}

[System.Serializable]
public class HighScoreUpdateRequest
{
    public int newScore;
}

[System.Serializable]
public class HighScoreUpdateResponse
{
    public int highScore;
}