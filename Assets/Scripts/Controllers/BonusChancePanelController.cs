using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusChancePanelController : MonoBehaviour
{
    public static event Action OnAnswerCorrect;

    private Transform _content;
    private HeartGameQuestion _question;

    private void Start()
    {
        _content = transform.GetChild(0);
    }

    private void OnEnable()
    {
        GameManager.OnUseBonusChance += FetchBonusRoundQuestion;
        HeartGameAPIClient.OnQuestionFetched += DisplayBonusQuestion;
        AnswerGuessController.OnAnswerGuessed += HandleAnswerGuess;
    }

    private void OnDisable()
    {
        GameManager.OnUseBonusChance -= FetchBonusRoundQuestion;
        HeartGameAPIClient.OnQuestionFetched -= DisplayBonusQuestion;
        AnswerGuessController.OnAnswerGuessed -= HandleAnswerGuess;
    }

    private void FetchBonusRoundQuestion()
    {
        HeartGameAPIClient.Instance.FetchQuestion();
    }

    void DisplayBonusQuestion(HeartGameQuestion question)
    {
        _question = question;

        Transform imageLayers = _content.GetChild(1);
        Transform answerGuessButtons = _content.GetChild(2).transform.GetChild(1);

        // Set image and background active
        imageLayers.GetChild(0).gameObject.SetActive(true);
        imageLayers.GetChild(1).gameObject.SetActive(true);

        // Set image
        imageLayers.GetChild(1).GetComponent<RawImage>().texture = question.texture;

        // Disable loading spinner
        imageLayers.GetChild(2).gameObject.SetActive(false);

        // Init answer array
        int[] answerGuesses = new int[3];
        answerGuesses[0] = question.heartsCount;
        answerGuesses[1] = UnityEngine.Random.Range(0, 9);
        answerGuesses[2] = UnityEngine.Random.Range(0, 9);

        // Shuffle answers (Modern Fisher-Yates algorithm)
        int unshuffledLength = answerGuesses.Length - 1;
        int temp;
        int i;
        for (i = unshuffledLength; i >= 1; i--)
        {
            int random = UnityEngine.Random.Range(0, unshuffledLength);
            temp = answerGuesses[random];
            answerGuesses[random] = answerGuesses[i];
            answerGuesses[i] = temp;
            unshuffledLength--;
        }

        // Set the answers
        for (i = 0; i < answerGuesses.Length; i++)
        {
            TextMeshProUGUI textObj = answerGuessButtons.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            textObj.text = answerGuesses[i].ToString();
        }

        // Display the panel
        _content.gameObject.SetActive(true);
    }

    void HandleAnswerGuess(int guessedAnswer)
    {
        if (guessedAnswer == _question.heartsCount)
        {
            OnAnswerCorrect?.Invoke();
            _content.gameObject.SetActive(false);
        }
    }
}
