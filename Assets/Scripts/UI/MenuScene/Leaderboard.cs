using UnityEngine;
using UnityEngine.UI;

// Leaderboard UI component
public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Transform _recordContainer;
    [SerializeField] private GameObject _recordPrefab;
    [SerializeField] private Button _btnBack;
    [SerializeField] private GameObject _loadingSpinner;

    private void Start()
    {
        _btnBack.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.LeaderboardBackButton)));

        // Enable loading spinner
        _loadingSpinner.SetActive(true);

        EventBus.Publish(new OnLeaderboardRequest());
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnLeaderboardRequestCompleted>(HandleLeaderboardRequestCompleted);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnLeaderboardRequestCompleted>(HandleLeaderboardRequestCompleted);
    }

    private void HandleLeaderboardRequestCompleted(OnLeaderboardRequestCompleted e)
    {
        // Clear leaderbaord
        for (int i = _recordContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(_recordContainer.GetChild(i).gameObject);
        }

        // Disable loading spinner
        _loadingSpinner.SetActive(false);

        // Add fetched records
        foreach (var r in e.Records)
        {
            GameObject record = Instantiate(_recordPrefab, _recordContainer);
            record.GetComponent<LeaderboardRecord>().InitRecord(r.PlayerName, r.PlayerScore);
        }
    }
}