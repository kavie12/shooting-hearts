using TMPro;
using UnityEngine;

public class LeaderboardRecord : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;

    public void InitRecord(string name, int score)
    {
        playerName.text = name;
        playerScore.text = score.ToString();
    }
}