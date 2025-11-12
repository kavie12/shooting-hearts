using System.Collections;
using UnityEngine;

public class BonusChanceManager : MonoBehaviour
{
    [SerializeField] private GameObject _bonusChancePanel;

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
            Invoke(nameof(DenyBonusChance), 3f);
            return;
        }

        StartCoroutine(HeartGameAPIClient.SendBonusChanceQuestionRequest(HandleBonusChanceQuestionFetched));
        StartCoroutine(DisplayPanel(3f));
    }

    private void HandleBonusChanceQuestionFetched(BonusChanceQuestion question, string error)
    {
        if (question != null)
        {
            _question = question;
        }
        else
        {
            Debug.Log(error);
        }
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
        if (_bonusChancePanel.activeSelf) ClosePanel();
        EventBus.Publish(new BonusChanceDeniedEvent());
    }

    private void ClosePanel()
    {
        _bonusChancePanel.SetActive(false);
    }
}