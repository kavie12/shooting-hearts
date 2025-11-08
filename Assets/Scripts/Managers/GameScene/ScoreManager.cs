using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score;

    private void OnEnable()
    {
        EventBus.Subscribe<EnemyDestroyedEvent>(IncreaseScore);
        EventBus.Subscribe<GameOverEvent>(PublishFinalScore);
        EventBus.Subscribe<OnHighScoreUpdateSuccessEvent>(HandleHighScoreUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<EnemyDestroyedEvent>(IncreaseScore);
        EventBus.Unsubscribe<GameOverEvent>(PublishFinalScore);
        EventBus.Unsubscribe<OnHighScoreUpdateSuccessEvent>(HandleHighScoreUpdate);
    }

    private void IncreaseScore(EnemyDestroyedEvent e)
    {
        _score += e.PointsEarned;
        EventBus.Publish(new ScoreUpdatedEvent(_score));
    }

    private void PublishFinalScore(GameOverEvent e)
    {
        EventBus.Publish(new UpdateFinalScoreEvent(_score));
    }

    private void HandleHighScoreUpdate(OnHighScoreUpdateSuccessEvent e)
    {
        EventBus.Publish(new OnFinalScoresReadyEvent(_score, e.HighScore));
    }
}