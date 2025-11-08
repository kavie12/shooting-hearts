using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerGuess : MonoBehaviour
{
    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => EventBus.Publish(new BonusChanceQuestionAnswerGuessEvent(int.Parse(_button.GetComponentInChildren<TextMeshProUGUI>().text))));
    }
}
