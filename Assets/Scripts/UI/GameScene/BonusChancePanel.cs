using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Panel that displays the bonus chance question with image and answer guesses
public class BonusChancePanel : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private GameObject _imageBackground;
    [SerializeField] private GameObject _loadingSpinner;
    [SerializeField] private GameObject answerGuessButtons;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private float _timer = 5f;

    private Coroutine _timerCoroutine;

    private void OnEnable()
    {
        EventBus.Subscribe<OnBonusChanceQuestionFetched>(HandleQuestionFetched);
        EventBus.Subscribe<OnBonusChanceQuestionAnswerGuessed>(HandleAnswerGuess);
        EventBus.Subscribe<OnBonusChanceQuestionAnswerChecked>(HandleBonusChanceQuestionAnswerChecked);
        EventBus.Subscribe<OnBonusChanceRequestCompleted>(HandleBonusChanceRequestComplete);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnBonusChanceQuestionFetched>(HandleQuestionFetched);
        EventBus.Unsubscribe<OnBonusChanceQuestionAnswerGuessed>(HandleAnswerGuess);
        EventBus.Unsubscribe<OnBonusChanceQuestionAnswerChecked>(HandleBonusChanceQuestionAnswerChecked);
        EventBus.Unsubscribe<OnBonusChanceRequestCompleted>(HandleBonusChanceRequestComplete);
    }

    #region Event Handlers

    private void HandleQuestionFetched(OnBonusChanceQuestionFetched e)
    {
        // Set image
        _image.texture = e.Question.ImageTexture;

        // Set image and background active
        _image.gameObject.SetActive(true);
        _imageBackground.SetActive(true);

        // Disable loading spinner
        _loadingSpinner.SetActive(false);

        // Init answer guesses
        int[] answerGuesses = CreateAnswerGuesses(answerGuessButtons.transform.childCount, e.Question.HeartsCount);

        // Set the answers in the GUI buttons
        for (int i = 0; i < answerGuessButtons.transform.childCount; i++)
        {
            TextMeshProUGUI textObj = answerGuessButtons.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            textObj.text = answerGuesses[i].ToString();
        }

        // Start the timer
        _timerCoroutine = StartCoroutine(RunTimer(_timer));
    }

    private void HandleAnswerGuess(OnBonusChanceQuestionAnswerGuessed e)
    {
        DisableAnswerButtons();
        StopCoroutine(_timerCoroutine);
    }

    private void HandleBonusChanceRequestComplete(OnBonusChanceRequestCompleted e)
    {
        gameObject.SetActive(false);
    }

    private void HandleBonusChanceQuestionAnswerChecked(OnBonusChanceQuestionAnswerChecked e)
    {
        if (!e.Correct)
        {
            _message.text = "Incorrect Answer.";
            _message.gameObject.SetActive(true);
        }
    }

    #endregion

    private int[] CreateAnswerGuesses(int numberOfAnswers, int correctAnswer)
    {
        int correctAnswerIndex = UnityEngine.Random.Range(0, numberOfAnswers - 1);

        int[] answerGuesses = new int[numberOfAnswers];
        answerGuesses[correctAnswerIndex] = correctAnswer;

        for (int i = 0; i < numberOfAnswers; i++)
        {
            if (i == correctAnswerIndex) continue;

            int rand = UnityEngine.Random.Range(0, 9);
            while (answerGuesses.Contains(rand))
            {
                rand = UnityEngine.Random.Range(0, 9);
            }
            answerGuesses[i] = rand;
        }

        return answerGuesses;
    }

    private void DisableAnswerButtons()
    {
        for (int i = 0; i < answerGuessButtons.transform.childCount; i++)
        {
            Button btn = answerGuessButtons.transform.GetChild(i).GetComponent<Button>();
            btn.interactable = false;
        }
    }

    private IEnumerator RunTimer(float duration)
    {
        float timeRemaining = duration;
        _timerSlider.maxValue = duration;
        _timerSlider.value = duration;

        while (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            _timerSlider.value = timeRemaining;
            _timerText.text = Mathf.Ceil(timeRemaining) + "s";
            yield return null;
        }

        HandleTimeout();
    }

    private void HandleTimeout()
    {
        DisableAnswerButtons();

        _message.text = "Time is over.";
        _message.gameObject.SetActive(true);

        EventBus.Publish(new OnBonusChanceQuestionTimeout());
    }
}
