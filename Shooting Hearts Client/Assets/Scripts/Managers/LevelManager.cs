using System.Collections;
using UnityEngine;

// Manage level progression, including loading levels, handling level completion, and managing intervals between levels.
public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _intervalBetwennLevels = 4f;

    private LevelConfig[] _levelConfigs;
    private int _currentLevelIndex;
    private Coroutine _levelProgressionCoroutine;

    private void OnEnable()
    {
        EventBus.Subscribe<OnGameStarted>(HandleGameStarted);
        EventBus.Subscribe<OnGameStopped>(HandleGameStopped);
        EventBus.Subscribe<OnGameContinued>(HandleGameContinued);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnGameStarted>(HandleGameStarted);
        EventBus.Unsubscribe<OnGameStopped>(HandleGameStopped);
        EventBus.Unsubscribe<OnGameContinued>(HandleGameContinued);
    }

    private void HandleGameStarted(OnGameStarted e)
    {
        _levelConfigs = e.GameConfig.LevelConfigs;

        _currentLevelIndex = 0;
        _levelProgressionCoroutine = StartCoroutine(LevelProgression());
    }

    private void HandleGameStopped(OnGameStopped e)
    {
        StopCoroutine(_levelProgressionCoroutine);
        EventBus.Publish(new OnLevelStopped());
    }

    private void HandleGameContinued(OnGameContinued e)
    {
        _levelProgressionCoroutine = StartCoroutine(LevelProgression());
        EventBus.Publish(new OnLevelRestarted());
    }

    private IEnumerator LevelProgression()
    {
        while (_currentLevelIndex < _levelConfigs.Length)
        {
            LoadLevel(_currentLevelIndex);
            EventBus.Publish(new OnLevelStarted(_levelConfigs[_currentLevelIndex].LevelName));
            yield return new WaitForSeconds(_levelConfigs[_currentLevelIndex].Duration);
            EventBus.Publish(new OnLevelCompleted());

            if (_currentLevelIndex == _levelConfigs.Length - 1)
            {
                EventBus.Publish(new OnAllLevelsCompleted());
                yield break;
            }

            yield return new WaitForSeconds(_intervalBetwennLevels);
            _currentLevelIndex++;
        }
    }

    private void LoadLevel(int index)
    {
        EventBus.Publish(new OnLevelLoaded(_levelConfigs[index]));
    }
}