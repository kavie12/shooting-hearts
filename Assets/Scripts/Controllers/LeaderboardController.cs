using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardRecordPrefab;

    private List<LeaderboardRecord> _leaderboardRecordList = new List<LeaderboardRecord>();

    LeaderboardController()
    {
        _leaderboardRecordList.Add(new LeaderboardRecord("Adolf Hitlar", 10500));
        _leaderboardRecordList.Add(new LeaderboardRecord("Albert Einstein", 9800));
        _leaderboardRecordList.Add(new LeaderboardRecord("Isacc Newton", 6200));
        _leaderboardRecordList.Add(new LeaderboardRecord("Donald Trump", 4000));
        _leaderboardRecordList.Add(new LeaderboardRecord("Vladamir Putin", 3800));
        _leaderboardRecordList.Add(new LeaderboardRecord("Kim Jon Un", 2000));
        _leaderboardRecordList.Add(new LeaderboardRecord("Kaveesha Dissanayake", 900));

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Display all the leaderboard records
        for (int i = 0; i < _leaderboardRecordList.Count; i++)
        {
            DisplayRecord(_leaderboardRecordList[i]);
        }
    }

    void DisplayRecord(LeaderboardRecord record)
    {
        GameObject obj = Instantiate(_leaderboardRecordPrefab, transform);
        LeaderboardRecordController lrc = obj.GetComponent<LeaderboardRecordController>();
        lrc.leaderboardRecord = record;
    }
}