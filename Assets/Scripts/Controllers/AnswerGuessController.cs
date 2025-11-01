using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerGuessController : MonoBehaviour
{
    public static event Action<int> OnAnswerGuessed;

    private Button _button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(InvokeClick);
    }

    void OnDestroy()
    {
        _button.onClick.RemoveListener(InvokeClick);
    }

    void InvokeClick()
    {
        OnAnswerGuessed?.Invoke(int.Parse(_button.GetComponentInChildren<TextMeshProUGUI>().text));
    }
}
