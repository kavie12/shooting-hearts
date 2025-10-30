using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerGuessController : MonoBehaviour
{
    public static event Action<int> OnAnswerGuessed;

    private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(InvokeClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(InvokeClick);
    }

    void InvokeClick()
    {
        OnAnswerGuessed?.Invoke(int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text));
    }
}
