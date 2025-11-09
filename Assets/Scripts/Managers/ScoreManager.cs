using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int[] _scoreInEachLevel;
    private int _currentLevelIndex;

    private int _finalScore = 0;

    private void OnEnable()
    {
        EventBus.Subscribe<GameStartEvent>(InitScore);
        EventBus.Subscribe<LevelLoadedEvent>(TrackLevel);
        EventBus.Subscribe<GameContinueEvent>(HandleLevelRestart);
        EventBus.Subscribe<EnemyDestroyedEvent>(IncreaseScore);
        EventBus.Subscribe<GameOverEvent>(HandleFinalScore);
        EventBus.Subscribe<OnHighScoreUpdateSuccessEvent>(HandleHighScoreUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartEvent>(InitScore);
        EventBus.Unsubscribe<LevelLoadedEvent>(TrackLevel);
        EventBus.Unsubscribe<GameContinueEvent>(HandleLevelRestart);
        EventBus.Unsubscribe<EnemyDestroyedEvent>(IncreaseScore);
        EventBus.Unsubscribe<GameOverEvent>(HandleFinalScore);
        EventBus.Unsubscribe<OnHighScoreUpdateSuccessEvent>(HandleHighScoreUpdate);
    }

    private void InitScore(GameStartEvent e)
    {
        _scoreInEachLevel = new int[e.GameConfig.LevelConfigs.Length];
    }

    private void HandleLevelRestart(GameContinueEvent e)
    {
        _finalScore -= _scoreInEachLevel[_currentLevelIndex];
        _scoreInEachLevel[_currentLevelIndex] = 0;
        EventBus.Publish(new ScoreUpdatedEvent(_finalScore));
    }

    private void TrackLevel(LevelLoadedEvent e)
    {
        _currentLevelIndex = e.LevelIndex;
    }

    private void IncreaseScore(EnemyDestroyedEvent e)
    {
        _scoreInEachLevel[_currentLevelIndex] += e.PointsEarned;
        _finalScore += e.PointsEarned;
        EventBus.Publish(new ScoreUpdatedEvent(_finalScore));
    }

    private void HandleFinalScore(GameOverEvent e)
    {
        EventBus.Publish(new UpdateFinalScoreEvent(_finalScore));
    }

    private void HandleHighScoreUpdate(OnHighScoreUpdateSuccessEvent e)
    {
        EventBus.Publish(new OnFinalScoresReadyEvent(_finalScore, e.HighScore));
    }
}