using TMPro;
using UnityEngine;

// Displays and updates the player's score in the game scene
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
        EventBus.Subscribe<OnScoreUpdated>(HandleScoreUpdated);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnScoreUpdated>(HandleScoreUpdated);
    }

    private void HandleScoreUpdated(OnScoreUpdated e)
    {
        _score.text = e.Score.ToString();
    }
}