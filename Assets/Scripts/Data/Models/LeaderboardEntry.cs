[System.Serializable]
// Data model representing an entry in the leaderboard
public class LeaderboardEntry
{
    public string PlayerName { get; }
    public int PlayerScore { get; }
    public LeaderboardEntry(string name, int score)
    {
        PlayerName = name;
        PlayerScore = score;
    }
}