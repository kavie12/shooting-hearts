using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusChancePanelController : MonoBehaviour
{
    public static event Action OnAnswerCorrect;

    private Transform content;
    private HeartGameQuestion question;

    private void Start()
    {
        content = transform.GetChild(0);
    }

    private void OnEnable()
    {
        SpaceshipController.OnDestroyed += FetchBonusRoundQuestion;
        HeartGameAPIClient.OnQuestionFetched += DisplayBonusQuestion;
        AnswerGuessController.OnAnswerGuessed += HandleAnswerGuess;
    }

    private void OnDisable()
    {
        SpaceshipController.OnDestroyed -= FetchBonusRoundQuestion;
        HeartGameAPIClient.OnQuestionFetched -= DisplayBonusQuestion;
        AnswerGuessController.OnAnswerGuessed -= HandleAnswerGuess;
    }

    private void FetchBonusRoundQuestion()
    {
        content.gameObject.SetActive(true);
        HeartGameAPIClient.Instance.FetchQuestion();
    }

    void DisplayBonusQuestion(HeartGameQuestion question)
    {
        this.question = question;

        Transform imageLayers = content.GetChild(1);
        Transform answerGuessButtons = content.GetChild(2).transform.GetChild(1);

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

        for (i = 0; i < answerGuesses.Length; i++)
        {
            TextMeshProUGUI textObj = answerGuessButtons.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            textObj.text = answerGuesses[i].ToString();
        }
    }

    void HandleAnswerGuess(int guessedAnswer)
    {
        bool isCorrect = guessedAnswer == question.heartsCount;

        if (isCorrect)
        {
            OnAnswerCorrect?.Invoke();
            content.gameObject.SetActive(false);
        }
    }
}
