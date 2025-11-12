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
        EventBus.Subscribe<MainMenuLeaderboardButtonClickEvent>(FetchLeaderboard);
        EventBus.Subscribe<UpdateFinalScoreEvent>(FetchUpdatedHighScore);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<MainMenuLeaderboardButtonClickEvent>(FetchLeaderboard);
        EventBus.Unsubscribe<UpdateFinalScoreEvent>(FetchUpdatedHighScore);
    }

    private void FetchLeaderboard(MainMenuLeaderboardButtonClickEvent e)
    {
        StartCoroutine(ApiClient.Get<LeaderboardResponse, LeaderboardErrorResponse>($"{_baseUrl}/leaderboard", HandleLeaderboardRequest, _authProvider.AccessToken));
    }

    private void FetchUpdatedHighScore(UpdateFinalScoreEvent e)
    {
        StartCoroutine(ApiClient.Post<UpdateHighScoreResponse, LeaderboardErrorResponse>($"{_baseUrl}/update-high-score", new UpdateHighScoreRequest { newScore = e.FinalScore }, HandlePlayerHighScoreUpdateRequest, _authProvider.AccessToken));
    }

    private void HandleLeaderboardRequest(LeaderboardResponse res, LeaderboardErrorResponse error)
    {
        if (res != null)
        {
            var entries = res.leaderboard.Select(r => new LeaderboardEntry(r.playerName, r.playerScore)).ToList();
            EventBus.Publish(new OnLeaderboardFetchSuccessEvent(entries));
        }
        else
        {
            Debug.Log(error);
            EventBus.Publish(new OnLeaderboardFetchFailedEvent(error.message));
        }
    }

    private void HandlePlayerHighScoreUpdateRequest(UpdateHighScoreResponse res, LeaderboardErrorResponse error)
    {
        if (res != null)
        {
            _highScore = res.highScore;
            EventBus.Publish(new OnHighScoreUpdateSuccessEvent(_highScore));
        }
        else
        {
            Debug.Log(error);
            EventBus.Publish(new OnHighScoreUpdateFailedEvent(error.message));
        }
    }
}