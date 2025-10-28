using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardRecordPrefab;

    private List<LeaderboardRecord> leaderboardRecordList = new List<LeaderboardRecord>();

    LeaderboardController()
    {
        leaderboardRecordList.Add(new LeaderboardRecord("Adolf Hitlar", 10500));
        leaderboardRecordList.Add(new LeaderboardRecord("Albert Einstein", 9800));
        leaderboardRecordList.Add(new LeaderboardRecord("Isacc Newton", 6200));
        leaderboardRecordList.Add(new LeaderboardRecord("Donald Trump", 4000));
        leaderboardRecordList.Add(new LeaderboardRecord("Vladamir Putin", 3800));
        leaderboardRecordList.Add(new LeaderboardRecord("Kim Jon Un", 2000));
        leaderboardRecordList.Add(new LeaderboardRecord("Kaveesha Dissanayake", 900));

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Display all the leaderboard records
        for (int i = 0; i < leaderboardRecordList.Count; i++)
        {
            displayRecord(leaderboardRecordList[i]);
        }
    }

    void displayRecord(LeaderboardRecord record)
    {
        GameObject obj = Instantiate(leaderboardRecordPrefab, transform);
        LeaderboardRecordController lrc = obj.GetComponent<LeaderboardRecordController>();
        lrc.leaderboardRecord = record;
    }
}