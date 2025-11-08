using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelConfig[] _levels;
    [SerializeField] private float _intervalBetwennLevels = 4f;

    private int _currentLevelIndex = 0;

    private Coroutine _levelProgressionCoroutine;

    private void OnEnable()
    {
        EventBus.Subscribe<GameStartEvent>(StartLevelProgression);
        EventBus.Subscribe<PlayerDiedEvent>(StopLevelProgression);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartEvent>(StartLevelProgression);
        EventBus.Unsubscribe<PlayerDiedEvent>(StopLevelProgression);
    }

    private void StartLevelProgression(GameStartEvent e)
    {
        _levelProgressionCoroutine = StartCoroutine(LevelProgression());
    }

    private void StopLevelProgression(PlayerDiedEvent e)
    {
        StopCoroutine(_levelProgressionCoroutine);
    }

    private IEnumerator LevelProgression()
    {
        while (_currentLevelIndex < _levels.Length)
        {
            LoadLevel(_currentLevelIndex);
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
        EventBus.Publish(new LevelLoadedEvent(_levels[index]));
    }

    private LevelConfig GetCurrentLevel() => _levels[_currentLevelIndex];
}