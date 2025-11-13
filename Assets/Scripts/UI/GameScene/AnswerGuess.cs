using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerGuess : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(() => EventBus.Publish(new BonusChanceQuestionAnswerGuessEvent(int.Parse(_button.GetComponentInChildren<TextMeshProUGUI>().text))));
    }
}
