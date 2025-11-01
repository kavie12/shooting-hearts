using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusChancePanelController : MonoBehaviour
{
    public static event Action OnAnswerCorrect;
    public static event Action OnAnswerIncorrect;

    private HeartGameQuestion _question;
    private Button _btnCancel;

    private void Awake()
    {
        _btnCancel = transform.GetChild(0).GetComponentInChildren<Button>();
        _btnCancel.onClick.AddListener(CancelBonusChance);

        FetchBonusRoundQuestion();
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

    private void OnDestroy()
    {
        _btnCancel.onClick.RemoveListener(CancelBonusChance);
    }

    private void FetchBonusRoundQuestion()
    {
        HeartGameAPIClient.instance.FetchQuestion();
    }

    void DisplayBonusQuestion(HeartGameQuestion question)
    {
        _question = question;

        Transform imageLayers = transform.GetChild(1);
        Transform answerGuessButtons = transform.GetChild(2).GetChild(1);

        // Set image
        imageLayers.GetChild(1).GetComponent<RawImage>().texture = question.texture;

        // Set image and background active
        imageLayers.GetChild(0).gameObject.SetActive(true);
        imageLayers.GetChild(1).gameObject.SetActive(true);

        // Disable loading spinner
        imageLayers.GetChild(2).gameObject.SetActive(false);

        // Init answer guesses
        int[] answerGuesses = new int[3];
        answerGuesses[0] = question.heartsCount;
        answerGuesses[1] = UnityEngine.Random.Range(0, 9);
        answerGuesses[2] = UnityEngine.Random.Range(0, 9);

        // Shuffle answer guesses in the array
        ShuffleAnswers(ref answerGuesses);

        // Set the answers in the GUI buttons
        for (int i = 0; i < answerGuesses.Length; i++)
        {
            TextMeshProUGUI textObj = answerGuessButtons.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            textObj.text = answerGuesses[i].ToString();
        }
    }

    void ShuffleAnswers(ref int[] answerGuesses)
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

    void CancelBonusChance()
    {
        gameObject.SetActive(false);
        Application.Quit();
    }

    void HandleQuestionFetchError(string error)
    {
        Debug.Log(error);
    }
}
