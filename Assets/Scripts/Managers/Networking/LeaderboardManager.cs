using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardManager : MonoBehaviour
{
    private readonly string _baseUrl = "http://localhost:3000/api/player";
    private int _highScore = 0;

    private IAuthTokenProvider _authTokenProvider;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _authTokenProvider = FindFirstObjectByType<AuthManager>();
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

    private void FetchUpdatedHighScore(UpdateFinalScoreEvent e)
    {
        StartCoroutine(SendPlayerHighScoreUpdateRequest(e.FinalScore));
    }

    private void FetchLeaderboard(MainMenuLeaderboardButtonClickEvent e)
    {
        StartCoroutine(SendLeaderboardRequest());
    }

    // Server check if the new score is higher than the current high score, update it on the databse, send the high score back
    private IEnumerator SendPlayerHighScoreUpdateRequest(int newScore)
    {
        string token = _authTokenProvider.GetToken();

        if (token == null || token == string.Empty)
        {
            Debug.Log("Auth token is null.");
            yield break;
        }

        string jsonData = JsonUtility.ToJson(new UpdateHighScoreRequest { newScore = newScore });

        using UnityWebRequest req = UnityWebRequest.Post($"{_baseUrl}/update-high-score", jsonData, "application/json");
        req.timeout = 10;
        req.SetRequestHeader("Authorization", "Bearer " + token);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            UpdateHighScoreResponse res = JsonUtility.FromJson<UpdateHighScoreResponse>(req.downloadHandler.text);
            _highScore = res.highScore;
            EventBus.Publish(new OnHighScoreUpdateSuccessEvent(_highScore));
        }
        else
        {
            Debug.Log(ParseError(req));
            EventBus.Publish(new OnHighScoreUpdateFailedEvent(ParseError(req)));
        }
    }

    private IEnumerator SendLeaderboardRequest()
    {
        string token = _authTokenProvider.GetToken();

        if (token == null)
        {
            Debug.Log("Auth token is null.");
            yield break;
        }

        using UnityWebRequest req = UnityWebRequest.Get($"{_baseUrl}/leaderboard");
        req.timeout = 10;
        req.SetRequestHeader("Authorization", "Bearer " + token);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            LeaderboardResponse res = JsonUtility.FromJson<LeaderboardResponse>(req.downloadHandler.text);

            var entries = res.leaderboard.Select(r => new LeaderboardEntry(r.playerName, r.playerScore)).ToList();

            EventBus.Publish(new OnLeaderboardFetchSuccessEvent(entries));
        }
        else
        {
            Debug.Log(ParseError(req));
            EventBus.Publish(new OnLeaderboardFetchFailedEvent(ParseError(req)));
        }
    }

    private string ParseError(UnityWebRequest req)
    {
        if (req.responseCode == 400)
        {
            return JsonUtility.FromJson<ErrorResponse>(req.downloadHandler.text).message;
        }
        else if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            return "Failed connection with server.";
        }
        return "An unexpected error occurred.";
    }
}