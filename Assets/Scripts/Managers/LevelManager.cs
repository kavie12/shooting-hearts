using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _intervalBetwennLevels = 4f;

    private LevelConfig[] _levels;
    private int _currentLevelIndex = 0;
    private Coroutine _levelProgressionCoroutine;

    private void OnEnable()
    {
        EventBus.Subscribe<GameStartEvent>(StartLevelProgression);
        EventBus.Subscribe<GameContinueEvent>(ContinueLevelProgression);
        EventBus.Subscribe<PlayerDestroyedEvent>(StopLevelProgression);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartEvent>(StartLevelProgression);
        EventBus.Unsubscribe<GameContinueEvent>(ContinueLevelProgression);
        EventBus.Unsubscribe<PlayerDestroyedEvent>(StopLevelProgression);
    }

    private void StartLevelProgression(GameStartEvent e)
    {
        _levels = e.GameConfig.LevelConfigs;

        _currentLevelIndex = 0;
        _levelProgressionCoroutine = StartCoroutine(LevelProgression());
    }

    private void ContinueLevelProgression(GameContinueEvent e)
    {
        _levelProgressionCoroutine = StartCoroutine(LevelProgression());
    }

    private void StopLevelProgression(PlayerDestroyedEvent e)
    {
        StopCoroutine(_levelProgressionCoroutine);
    }

    private IEnumerator LevelProgression()
    {
        while (_currentLevelIndex < _levels.Length)
        {
            LoadLevel(_currentLevelIndex);
            EventBus.Publish(new LevelStartedEvent(_currentLevelIndex, GetCurrentLevel()));
            yield return new WaitForSeconds(GetCurrentLevel().Duration);
            EventBus.Publish(new LevelCompletedEvent());

            if (_currentLevelIndex == _levels.Length - 1)
            {
                EventBus.Publish(new AllLevelsCompletedEvent());
                yield break;
            }

            yield return new WaitForSeconds(_intervalBetwennLevels);
            _currentLevelIndex++;
        }
    }

    private void LoadLevel(int index)
    {
        EventBus.Publish(new LevelLoadedEvent(index, _levels[index]));
        EventBus.Publish(new ShowAlertEvent(_levels[index].Hint, 4f));
    }

    private LevelConfig GetCurrentLevel() => _levels[_currentLevelIndex];
}