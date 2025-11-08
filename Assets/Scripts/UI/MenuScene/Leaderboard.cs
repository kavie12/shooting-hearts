using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Transform _recordContainer;
    [SerializeField] private GameObject _recordPrefab;
    [SerializeField] private Button _btnBack;

    private void Start()
    {
        _btnBack.onClick.AddListener(() => EventBus.Publish(new LeaderboardBackButtonClickEvent()));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnLeaderboardFetchSuccessEvent>(HandleLeaderboardFetchSuccess);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnLeaderboardFetchSuccessEvent>(HandleLeaderboardFetchSuccess);
    }

    private void HandleLeaderboardFetchSuccess(OnLeaderboardFetchSuccessEvent eventData)
    {
        foreach (var r in eventData.records)
        {
            GameObject record = Instantiate(_recordPrefab, _recordContainer);
            record.GetComponent<LeaderboardRecord>().InitRecord(r.playerName, r.playerScore);
        }
    }
}