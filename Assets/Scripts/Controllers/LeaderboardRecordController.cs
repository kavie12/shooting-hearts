using TMPro;
using UnityEngine;

public class LeaderboardRecordController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameObject;
    [SerializeField] private TextMeshProUGUI _playerScoreObject;

    public LeaderboardRecord leaderboardRecord;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerNameObject.text = leaderboardRecord.name;
        _playerScoreObject.text = leaderboardRecord.score.ToString();
    }
}
