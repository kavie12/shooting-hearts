using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : BaseMenuState
{
    [SerializeField] private Transform _recordContainer;
    [SerializeField] private GameObject _recordPrefab;
    [SerializeField] private Button _btnBack;

    private readonly List<LeaderboardRecord> _leaderboardRecordList = new();

    void Start()
    {
        _btnBack.onClick.AddListener(() => StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.MAIN_MENU)));

        // Display all the leaderboard records
        for (int i = 0; i < _leaderboardRecordList.Count; i++)
        {
            DisplayRecord(_leaderboardRecordList[i]);
        }
    }

    void DisplayRecord(LeaderboardRecord record)
    {
        GameObject leaderboardRecord = Instantiate(_recordPrefab, _recordContainer);
        //record.GetComponent<LeaderboardRecord>().playerName =
    }
}
