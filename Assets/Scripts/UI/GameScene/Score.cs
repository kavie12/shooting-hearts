using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI _score;

    private void Awake()
    {
        _score = GetComponent<TextMeshProUGUI>();
        _score.text = "0";
    }

    private void OnEnable()
    {
        GameManager.OnScoreUpdated += HandleScoreUpdate;
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdated -= HandleScoreUpdate;
    }

    private void HandleScoreUpdate(int score)
    {
        _score.text = score.ToString();
    }
}