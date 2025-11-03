using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusChancePanelController : MonoBehaviour
{
    public static event Action OnAnswerCorrect;
    public static event Action OnAnswerIncorrect;
    public static event Action OnTimeout;

    [SerializeField] private RawImage _image;
    [SerializeField] private GameObject _imageBackground;
    [SerializeField] private GameObject _loadingSpinner;
    [SerializeField] private GameObject answerGuessButtons;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _timerSlider;

    private HeartGameQuestion _question;
    private float _timer = 5f;
    private bool _isTimeOut = false;

    private void Start()
    {
        FetchBonusRoundQuestion();

        // Display alert
        AlertController.instance.ShowAlert("Bonus Chance Challenge");
    }

    private void Update()
    {
        // Update timer
        if (_question != null && !_isTimeOut)
        {
            if (_timer >= 0f)
            {
                _timer -= Time.deltaTime;
                _timerSlider.value = _timer;
                _timerText.text = Math.Ceiling(_timer).ToString() + "s";
            }
            else
            {
                OnTimeout?.Invoke();
                _isTimeOut = true;
            }
        }
    }

    private void OnEnable()
    {
        HeartGameAPIClient.OnQuestionFetched += DisplayBonusQuestion;
        HeartGameAPIClient.OnQuestionFetchFailed += HandleQuestionFetchError;

        AnswerGuessController.OnAnswerGuessed += HandleAnswerGuess;
    }

    private void OnDisable()
    {
        HeartGameAPIClient.OnQuestionFetched -= DisplayBonusQuestion;
        HeartGameAPIClient.OnQuestionFetchFailed += HandleQuestionFetchError;

        AnswerGuessController.OnAnswerGuessed -= HandleAnswerGuess;
    }

    private void FetchBonusRoundQuestion()
    {
        HeartGameAPIClient.instance.FetchQuestion();
    }

    void DisplayBonusQuestion(HeartGameQuestion question)
    {
        _question = question;

        // Set image
        _image.texture = question.texture;

        // Set image and background active
        _image.gameObject.SetActive(true);
        _imageBackground.SetActive(true);

        // Disable loading spinner
        _loadingSpinner.SetActive(false);

        // Init answer guesses
        int[] answerGuesses = new int[3];
        answerGuesses[0] = question.heartsCount;
        answerGuesses[1] = UnityEngine.Random.Range(0, 9);
        answerGuesses[2] = UnityEngine.Random.Range(0, 9);

        // Shuffle answer guesses in the array
        ShuffleAnswers(answerGuesses);

        // Set the answers in the GUI buttons
        for (int i = 0; i < answerGuesses.Length; i++)
        {
            TextMeshProUGUI textObj = answerGuessButtons.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            textObj.text = answerGuesses[i].ToString();
        }
    }

    void ShuffleAnswers(int[] answerGuesses)
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

    void HandleAnswerGuess(int guessedAnswer)
    {
        if (guessedAnswer == _question.heartsCount)
        {
            OnAnswerCorrect?.Invoke();
        }
        else
        {
            OnAnswerIncorrect?.Invoke();
        }
        gameObject.SetActive(false);
    }

    void HandleQuestionFetchError(string error)
    {
        Debug.Log(error);
        Application.Quit();
    }
}
