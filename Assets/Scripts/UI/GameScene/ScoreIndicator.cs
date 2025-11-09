using TMPro;
using UnityEngine;

public class ScoreIndicator : MonoBehaviour
{
    private TextMeshProUGUI _score;

    private void Awake()
    {
        _score = GetComponent<TextMeshProUGUI>();
        _score.text = "0";
    }

    private void OnEnable()
    {
        EventBus.Subscribe<ScoreUpdatedEvent>(HandleScoreUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<ScoreUpdatedEvent>(HandleScoreUpdate);
    }

    private void HandleScoreUpdate(ScoreUpdatedEvent e)
    {
        _score.text = e.Score.ToString();
    }
}