using System.Collections;
using UnityEngine;

public class BonusChanceManager : MonoBehaviour
{
    [SerializeField] private GameObject _bonusChancePanel;

    private BonusChanceQuestion _question;
    private bool _isBonusChanceUsed;

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerDiedEvent>(HandleBonusChanceQuestion);
        EventBus.Subscribe<BonusChanceQuestionFetchSuccessEvent>(HandleQuestionFetch);
        EventBus.Subscribe<BonusChanceQuestionAnswerGuessEvent>(HandleAnswerGuess);
        EventBus.Subscribe<BonusChanceQuestionTimeout>(HandleTimeout);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerDiedEvent>(HandleBonusChanceQuestion);
        EventBus.Unsubscribe<BonusChanceQuestionFetchSuccessEvent>(HandleQuestionFetch);
        EventBus.Unsubscribe<BonusChanceQuestionAnswerGuessEvent>(HandleAnswerGuess);
        EventBus.Unsubscribe<BonusChanceQuestionTimeout>(HandleTimeout);
    }

    private void HandleBonusChanceQuestion(PlayerDiedEvent e)
    {
        if (_isBonusChanceUsed)
        {
            EventBus.Publish(new BonusChanceDeniedEvent());
            return;
        }

        _isBonusChanceUsed = true;
        _bonusChancePanel.SetActive(true);
        EventBus.Publish(new FetchBonusChanceQuestionEvent());
    }

    private void HandleQuestionFetch(BonusChanceQuestionFetchSuccessEvent e)
    {
        _question = e.Question;
    }

    private void HandleAnswerGuess(BonusChanceQuestionAnswerGuessEvent e)
    {
        if (e.GuessedAnswer == _question.HeartsCount)
        {
            HandleAnswerCorrect();
        }
        else
        {
            StartCoroutine(HandleAnswerIncorrect());
        }
    }

    private void HandleAnswerCorrect()
    {
        ClosePanel();
        EventBus.Publish(new BonusChanceGrantedEvent());
    }

    private IEnumerator HandleAnswerIncorrect()
    {
        EventBus.Publish(new ShowAlertEvent("Incorrect answer.", 2f));
        yield return new WaitForSeconds(2f);
        ClosePanel();
        EventBus.Publish(new BonusChanceDeniedEvent());
    }

    private void HandleTimeout(BonusChanceQuestionTimeout e)
    {
        StartCoroutine(HandleTimeout());
    }

    private IEnumerator HandleTimeout()
    {
        EventBus.Publish(new ShowAlertEvent("Time is over.", 2f));
        yield return new WaitForSeconds(2f);
        ClosePanel();
        EventBus.Publish(new BonusChanceDeniedEvent());
    }

    private void ClosePanel()
    {
        _bonusChancePanel.SetActive(false);
    }
}