using TMPro;
using UnityEngine;

public class LeaderboardRecordController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameObject;
    [SerializeField] private TextMeshProUGUI playerScoreObject;

    public LeaderboardRecord leaderboardRecord;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerNameObject.text = leaderboardRecord.name;
        playerScoreObject.text = leaderboardRecord.score.ToString();
    }
}
