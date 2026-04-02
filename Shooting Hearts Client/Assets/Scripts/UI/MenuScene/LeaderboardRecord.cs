using TMPro;
using UnityEngine;

// LeaderboardRecord is responsible for displaying a player's name and score in the leaderboard UI.
public class LeaderboardRecord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _playerScore;

    public void InitRecord(string name, int score)
    {
        _playerName.text = name;
        _playerScore.text = score.ToString();
    }
}