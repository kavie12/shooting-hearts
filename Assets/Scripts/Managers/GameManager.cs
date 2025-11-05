using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action StartLevelProgression;
    public static event Action StopLevelProgression;
    public static event Action<int> OnScoreUpdated;

    [SerializeField] private GameObject _spaceshipPrefab;
    [SerializeField] private GameObject _bonusChancePanel;

    private int _score = 0;
    private bool _bonusChanceUsed = false;

    private void Start()
    {
        StartLevelProgression?.Invoke();
        SpawnSpaceship();
    }

    private void OnEnable()
    {
        LevelManager.OnAllLevelsCompleted += QuitGame;

        EnemyObjectController.OnDestroyed += IncreaseScore;
        SpaceshipController.OnDestroyed += HandleSpaceshipDestroyed;

        BonusChancePanel.OnAnswerCorrect += GiveBonusChance;
        BonusChancePanel.OnAnswerIncorrect += QuitGame;
        BonusChancePanel.OnTimeout += QuitGame;
    }

    private void OnDisable()
    {
        LevelManager.OnAllLevelsCompleted += QuitGame;

        EnemyObjectController.OnDestroyed -= IncreaseScore;
        SpaceshipController.OnDestroyed -= HandleSpaceshipDestroyed;

        BonusChancePanel.OnAnswerCorrect -= GiveBonusChance;
        BonusChancePanel.OnAnswerIncorrect -= QuitGame;
        BonusChancePanel.OnTimeout -= QuitGame;
    }

    private void IncreaseScore(int score)
    {
        _score += score;
        OnScoreUpdated?.Invoke(_score);
    }

    private void SpawnSpaceship()
    {
        Instantiate(_spaceshipPrefab);
    }

    private void HandleSpaceshipDestroyed()
    {
        StopLevelProgression?.Invoke();
        if (!_bonusChanceUsed)
        {
            Invoke(nameof(ShowBonusChanceQuestionPanel), 2f);
            _bonusChanceUsed = true;
        }
        else
        {
            Invoke(nameof(QuitGame), 3f);
        }
    }

    private void ShowBonusChanceQuestionPanel()
    {
        _bonusChancePanel.SetActive(true);
    }

    private void GiveBonusChance()
    {
        StartLevelProgression?.Invoke();
        SpawnSpaceship();
    }

    private void QuitGame()
    {
        StopLevelProgression?.Invoke();
        Application.Quit();
    }
}