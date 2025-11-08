using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Transform _recordContainer;
    [SerializeField] private GameObject _recordPrefab;
    [SerializeField] private Button _btnBack;
    [SerializeField] private GameObject _loadingSpinner;

    private void Start()
    {
        _btnBack.onClick.AddListener(() => EventBus.Publish(new LeaderboardBackButtonClickEvent()));

        // Enable loading spinner
        _loadingSpinner.SetActive(true);
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
        // Clear leaderbaord
        for (int i = _recordContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(_recordContainer.GetChild(i).gameObject);
        }

        // Disable loading spinner
        _loadingSpinner.SetActive(false);

        // Add fetched records
        foreach (var r in eventData.records)
        {
            GameObject record = Instantiate(_recordPrefab, _recordContainer);
            record.GetComponent<LeaderboardRecord>().InitRecord(r.playerName, r.playerScore);
        }
    }
}