using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BonusChanceManager : MonoBehaviour
{
    [SerializeField] private GameObject _bonusChancePanel;

    private readonly string _apiUrl = "https://marcconrad.com/uob/heart/api.php";

    private BonusChanceQuestion _question = null;

    private bool _isBonusChanceUsed = false;

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerDestroyedEvent>(HandleBonusChanceQuestion);
        EventBus.Subscribe<BonusChanceQuestionAnswerGuessEvent>(HandleAnswerGuess);
        EventBus.Subscribe<BonusChanceQuestionTimeout>(HandleTimeout);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerDestroyedEvent>(HandleBonusChanceQuestion);
        EventBus.Unsubscribe<BonusChanceQuestionAnswerGuessEvent>(HandleAnswerGuess);
        EventBus.Unsubscribe<BonusChanceQuestionTimeout>(HandleTimeout);
    }

    private void HandleBonusChanceQuestion(PlayerDestroyedEvent e)
    {
        if (_isBonusChanceUsed)
        {
            Invoke(nameof(DenyBonusChance), 2f);
            return;
        }

        StartCoroutine(SendBonusChanceQuestionRequest());
        StartCoroutine(DisplayPanel(2f));
    }

    private void HandleAnswerGuess(BonusChanceQuestionAnswerGuessEvent e)
    {
        if (e.GuessedAnswer == _question.HeartsCount)
        {
            HandleAnswerCorrect();
        }
        else
        {
            HandleAnswerIncorrect();
        }
    }

    private void HandleAnswerCorrect()
    {
        ClosePanel();
        EventBus.Publish(new BonusChanceGrantedEvent());
    }

    private void HandleAnswerIncorrect()
    {
        EventBus.Publish(new ShowAlertEvent("Incorrect answer.", 2f));
        Invoke(nameof(DenyBonusChance), 2f);
    }

    private void HandleTimeout(BonusChanceQuestionTimeout e)
    {
        EventBus.Publish(new ShowAlertEvent("Time is over.", 2f));
        Invoke(nameof(DenyBonusChance), 2f);
    }

    private IEnumerator DisplayPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        _bonusChancePanel.SetActive(true);

        yield return new WaitUntil(() => _question != null);
        EventBus.Publish(new BonusChanceQuestionDisplayEvent(_question));
        _isBonusChanceUsed = true;
    }

    private void DenyBonusChance()
    {
        ClosePanel();
        EventBus.Publish(new BonusChanceDeniedEvent());
    }

    private void ClosePanel()
    {
        _bonusChancePanel.SetActive(false);
    }

    private IEnumerator SendBonusChanceQuestionRequest()
    {
        using UnityWebRequest req = UnityWebRequest.Get(_apiUrl);
        req.timeout = 10;
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            BonusChanceQuestionResponse question = JsonUtility.FromJson<BonusChanceQuestionResponse>(req.downloadHandler.text);
            StartCoroutine(FetchImageTexture(question));
        }
        else
        {
            Debug.Log(ParseError(req));
            Invoke(nameof(DenyBonusChance), 2f);
        }
    }

    private IEnumerator FetchImageTexture(BonusChanceQuestionResponse apiResponse)
    {
        using UnityWebRequest req = UnityWebRequestTexture.GetTexture(apiResponse.question);
        req.timeout = 10;
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(req);
            _question = new BonusChanceQuestion(texture, apiResponse.solution, apiResponse.carrots);
        }
        else
        {
            Debug.Log(ParseError(req));
            Invoke(nameof(DenyBonusChance), 2f);
        }
    }

    private string ParseError(UnityWebRequest req)
    {
        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            return "Failed connection with server.";
        }
        return "An unexpected error occurred.";
    }
}