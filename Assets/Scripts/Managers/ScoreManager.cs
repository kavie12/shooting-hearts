using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Dictionary<string, int> _scoreInEachLevel;
    private string _currentLevelName;

    private int _finalScore = 0;

    private void OnEnable()
    {
        EventBus.Subscribe<OnGameStarted>(HandleGameStarted);
        EventBus.Subscribe<OnLevelLoaded>(HandleLevelLoaded);
        EventBus.Subscribe<OnEnemyDestroyed>(HandleEnemyDestroyed);
        EventBus.Subscribe<OnLevelRestarted>(HandleLevelRestarted);
        EventBus.Subscribe<OnGameOver>(HandleGameOver);
        EventBus.Subscribe<OnHighScoreUpdateRequestCompleted>(HandleHighScoreUpdateRequestComplete);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnGameStarted>(HandleGameStarted);
        EventBus.Unsubscribe<OnLevelLoaded>(HandleLevelLoaded);
        EventBus.Unsubscribe<OnEnemyDestroyed>(HandleEnemyDestroyed);
        EventBus.Unsubscribe<OnLevelRestarted>(HandleLevelRestarted);
        EventBus.Unsubscribe<OnGameOver>(HandleGameOver);
        EventBus.Unsubscribe<OnHighScoreUpdateRequestCompleted>(HandleHighScoreUpdateRequestComplete);
    }

    private void HandleGameStarted(OnGameStarted e)
    {
        _scoreInEachLevel = new Dictionary<string, int>();

        foreach (var levelConfig in e.GameConfig.LevelConfigs)
        {
            _scoreInEachLevel.Add(levelConfig.LevelName, 0);
        }
    }

    private void HandleLevelLoaded(OnLevelLoaded e)
    {
        _currentLevelName = e.LevelConfig.LevelName;
    }

    private void HandleEnemyDestroyed(OnEnemyDestroyed e)
    {
        _scoreInEachLevel[_currentLevelName] += e.PointsEarned;
        _finalScore += e.PointsEarned;
        EventBus.Publish(new OnScoreUpdated(_finalScore));
    }

    private void HandleLevelRestarted(OnLevelRestarted e)
    {
        _finalScore -= _scoreInEachLevel[_currentLevelName];
        _scoreInEachLevel[_currentLevelName] = 0;

        EventBus.Publish(new OnScoreUpdated(_finalScore));
    }

    private void HandleGameOver(OnGameOver e)
    {
        EventBus.Publish(new OnHighScoreUpdateRequested(_finalScore));
    }

    private void HandleHighScoreUpdateRequestComplete(OnHighScoreUpdateRequestCompleted e)
    {
        if (e.Success)
        {
            EventBus.Publish(new OnUpdatedHighScoreFetched(_finalScore, e.HighScore));
        }
        else
        {
            Debug.Log("OnHighScoreUpdateRequestCompleted.Success=false");
            EventBus.Publish(new OnUpdatedHighScoreFetched(0, 0));
        }
    }
}