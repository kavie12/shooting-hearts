using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusChancePanel : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private GameObject _imageBackground;
    [SerializeField] private GameObject _loadingSpinner;
    [SerializeField] private GameObject answerGuessButtons;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private float _timer = 5f;

    private Coroutine _timerCoroutine;

    private void OnEnable()
    {
        EventBus.Subscribe<BonusChanceQuestionFetchSuccessEvent>(DisplayBonusQuestion);
        EventBus.Subscribe<BonusChanceQuestionAnswerGuessEvent>(HandleAnswerGuess);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<BonusChanceQuestionFetchSuccessEvent>(DisplayBonusQuestion);
        EventBus.Unsubscribe<BonusChanceQuestionAnswerGuessEvent>(HandleAnswerGuess);
    }

    private void DisplayBonusQuestion(BonusChanceQuestionFetchSuccessEvent e)
    {
        // Set image
        _image.texture = e.Question.ImageTexture;

        // Set image and background active
        _image.gameObject.SetActive(true);
        _imageBackground.SetActive(true);

        // Disable loading spinner
        _loadingSpinner.SetActive(false);

        // Init answer guesses
        int[] answerGuesses = new int[answerGuessButtons.transform.childCount];
        answerGuesses[0] = e.Question.HeartsCount;
        for (int i = 1; i < answerGuesses.Length; i++)
        {
            answerGuesses[i] = UnityEngine.Random.Range(0, 9);
        }

        // Shuffle answer guesses in the array
        ShuffleAnswers(answerGuesses);

        // Set the answers in the GUI buttons
        for (int i = 0; i < answerGuessButtons.transform.childCount; i++)
        {
            TextMeshProUGUI textObj = answerGuessButtons.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            textObj.text = answerGuesses[i].ToString();
        }

        // Start the timer
        _timerCoroutine = StartCoroutine(RunTimer(_timer));
    }

    private void HandleAnswerGuess(BonusChanceQuestionAnswerGuessEvent e)
    {
        DisableAnswerButtons();
        StopCoroutine(_timerCoroutine);
    }

    private void ShuffleAnswers(int[] answerGuesses)
    {
        // Shuffle answers (Modern Fisher-Yates algorithm)
        int unshuffledLength = answerGuesses.Length - 1;
        int temp;
        for (int i = unshuffledLength; i >= 1; i--)
        {
            int random = UnityEngine.Random.Range(0, unshuffledLength);
            temp = answerGuesses[random];
            answerGuesses[random] = answerGuesses[i];
            answerGuesses[i] = temp;
            unshuffledLength--;
        }
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
        EventBus.Publish(new BonusChanceQuestionTimeout());
    }
}
