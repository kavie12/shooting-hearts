using System.Collections;
using UnityEngine;

public class BonusChanceManager : MonoBehaviour
{
    private bool _isBonusChanceUsed = false;
    private BonusChanceQuestion _question = null;

    private void OnEnable()
    {
        EventBus.Subscribe<OnBonusChanceRequested>(HandleBonusChanceRequest);
        EventBus.Subscribe<OnBonusChanceQuestionAnswerGuessed>(HandleAnswerGuess);
        EventBus.Subscribe<OnBonusChanceQuestionTimeout>(HandleTimeout);
    }

    private void OnDisable()
    {
        EventBus.Subscribe<OnBonusChanceRequested>(HandleBonusChanceRequest);
        EventBus.Unsubscribe<OnBonusChanceQuestionAnswerGuessed>(HandleAnswerGuess);
        EventBus.Unsubscribe<OnBonusChanceQuestionTimeout>(HandleTimeout);
    }

    private void HandleBonusChanceRequest(OnBonusChanceRequested e)
    {
        if (_isBonusChanceUsed)
        {
            DenyBonusChance();
            return;
        }

        StartCoroutine(HeartGameApiClient.SendBonusChanceQuestionRequest(HandleBonusChanceQuestionFetched));
        StartCoroutine(DisplayQuestion());
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
            DenyBonusChance();
        }
    }

    #region Handle Answer Guess Functions

    private void HandleAnswerGuess(OnBonusChanceQuestionAnswerGuessed e)
    {
        if (e.GuessedAnswer == _question.HeartsCount) HandleAnswerCorrect();
        else HandleAnswerIncorrect();
    }

    private void HandleAnswerCorrect()
    {
        EventBus.Publish(new OnBonusChanceRequestCompleted(true));
    }

    private void HandleAnswerIncorrect()
    {
        EventBus.Publish(new ShowAlertEvent("Incorrect answer.", 2f));
        Invoke(nameof(DenyBonusChance), 2f);
    }

    private void HandleTimeout(OnBonusChanceQuestionTimeout e)
    {
        EventBus.Publish(new ShowAlertEvent("Time is over.", 2f));
        Invoke(nameof(DenyBonusChance), 2f);
    }

    #endregion

    private IEnumerator DisplayQuestion()
    {
        yield return new WaitUntil(() => _question != null);
        EventBus.Publish(new OnBonusChanceQuestionFetched(_question));
        _isBonusChanceUsed = true;
    }

    private void DenyBonusChance()
    {
        EventBus.Publish(new OnBonusChanceRequestCompleted(false));
    }
}