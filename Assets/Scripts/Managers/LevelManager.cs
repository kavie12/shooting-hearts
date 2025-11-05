using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event Action<LevelConfig> OnLevelStarted;
    public static event Action OnLevelEnded;
    public static event Action OnAllLevelsCompleted;

    [SerializeField] private LevelConfig[] _levelConfigs;
    [SerializeField] private float _intervalBetweenLevels = 5f;
    [SerializeField] private TextMeshProUGUI _levelIndicator;

    private int _currentLevelIndex = 0;

    private void OnEnable()
    {
        GameManager.StartLevelProgression += StartLevelProgression;
        GameManager.StopLevelProgression += StopLevelProgression;
    }

    private void OnDisable()
    {
        GameManager.StartLevelProgression -= StartLevelProgression;
        GameManager.StopLevelProgression -= StopLevelProgression;
    }

    private void StartLevelProgression()
    {
        StartCoroutine(LevelProgression());
    }

    private void StopLevelProgression()
    {
        StopAllCoroutines();
    }

    private IEnumerator LevelProgression()
    {
        while (_currentLevelIndex < _levelConfigs.Length)
        {
            LoadLevel();

            yield return new WaitForSeconds(_levelConfigs[_currentLevelIndex].duration);
            OnLevelEnded?.Invoke();

            if (_currentLevelIndex == _levelConfigs.Length - 1)
            {
                OnAllLevelsCompleted?.Invoke();
            }

            yield return new WaitForSeconds(_intervalBetweenLevels);

            _currentLevelIndex++;
        }
    }

    private void LoadLevel()
    {
        OnLevelStarted?.Invoke(_levelConfigs[_currentLevelIndex]);
        StartCoroutine(DisplayLevelIndicator(_levelConfigs[_currentLevelIndex].levelName));
        Invoke(nameof(DisplayLevelAlerts), 1f);
    }

    private IEnumerator DisplayLevelIndicator(string levelName)
    {
        _levelIndicator.text = levelName;
        _levelIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _levelIndicator.gameObject.SetActive(false);
    }

    private void DisplayLevelAlerts()
    {
        string message = "";
        switch (_currentLevelIndex)
        {
            case 0:
                message = "100 points for a carrot. Crashing 5 carrots will destroy the spaceship.";
                break;
            case 1:
                message = "500 points for a hearts. Crashing a heart will destroy the spaceship.";
                break;
            case 2:
                message = "Ready for a rain of hearts and carrots !!!";
                break;
            default:
                message = "";
                break;
        }
        Alert.instance.ShowAlert(message, 4f);
    }
}