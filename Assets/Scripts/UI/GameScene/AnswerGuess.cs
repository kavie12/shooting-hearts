using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerGuess : MonoBehaviour
{
    public static event Action<int> OnAnswerGuessed;

    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnAnswerGuessed?.Invoke(int.Parse(_button.GetComponentInChildren<TextMeshProUGUI>().text)));
    }
}
