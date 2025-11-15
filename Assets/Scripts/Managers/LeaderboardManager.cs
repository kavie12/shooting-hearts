using System.Linq;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private static LeaderboardManager instance;

    private readonly string _baseUrl = "http://localhost:3000/api/player";
    private int _highScore = 0;
    private IAuthProvider _authProvider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        _authProvider = FindFirstObjectByType<AuthManager>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnLeaderboardRequest>(HandleLeaderboardRequest);
        EventBus.Subscribe<OnHighScoreUpdateRequested>(HandleHighScoreUpdateRequest);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnLeaderboardRequest>(HandleLeaderboardRequest);
        EventBus.Unsubscribe<OnHighScoreUpdateRequested>(HandleHighScoreUpdateRequest);
    }

    private void HandleLeaderboardRequest(OnLeaderboardRequest e)
    {
        StartCoroutine(ApiClient.Get<LeaderboardResponse, LeaderboardErrorResponse>($"{_baseUrl}/leaderboard", HandleLeaderboardRequestComplete, _authProvider.AccessToken));
    }

    private void HandleHighScoreUpdateRequest(OnHighScoreUpdateRequested e)
    {
        StartCoroutine(ApiClient.Post<HighScoreUpdateResponse, LeaderboardErrorResponse>($"{_baseUrl}/update-high-score", new HighScoreUpdateRequest { newScore = e.NewScore }, HandleHighScoreUpdateRequestCompleted, _authProvider.AccessToken));
    }

    private void HandleLeaderboardRequestComplete(LeaderboardResponse res, LeaderboardErrorResponse error)
    {
        if (res != null)
        {
            var entries = res.leaderboard.Select(r => new LeaderboardEntry(r.playerName, r.playerScore)).ToList();
            EventBus.Publish(new OnLeaderboardRequestCompleted(true, entries));
        }
        else
        {
            Debug.Log(error.message);
            EventBus.Publish(new OnLeaderboardRequestCompleted(false, null));
        }
    }

    private void HandleHighScoreUpdateRequestCompleted(HighScoreUpdateResponse res, LeaderboardErrorResponse error)
    {
        if (res != null)
        {
            _highScore = res.highScore;
            EventBus.Publish(new OnHighScoreUpdateRequestCompleted(true, _highScore));
        }
        else
        {
            Debug.Log(error.message);
            EventBus.Publish(new OnHighScoreUpdateRequestCompleted(false, 0));
        }
    }
}