[System.Serializable]
public class LeaderboardEntry
{
    public string playerName { get; }
    public int playerScore { get; }

    public LeaderboardEntry(string name, int score)
    {
        playerName = name;
        playerScore = score;
    }
}