using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static event Action<LevelConfig> OnLevelChanged;
    public static event Action OnGameOver;

    [Header("Level Config")]
    [SerializeField] private float _levelDuration = 16f;
    [SerializeField] private LevelConfig[] _levels;

    [SerializeField] private TextMeshProUGUI _levelIndicator;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _spaceshipPrefab;
    [SerializeField] private GameObject _bonusChancePanel;

    private int _score = 0;
    private bool _bonusChanceUsed = false;
    private int _currentLevelIndex;
    private Coroutine _levelProgressionCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()    
    {
        // Init levels
        LoadLevel(0);
        _levelProgressionCoroutine = StartCoroutine(LevelProgression(_levelDuration));

        SpawnSpaceship();

        _scoreText.text = _score.ToString();
    }

    private void OnEnable()
    {
        EnemyObjectController.OnDestroyed += IncreaseScore;
        SpaceshipController.OnDestroyed += HandleSpaceshipDestroyed;

        BonusChancePanelController.OnAnswerCorrect += GiveBonusChance;
        BonusChancePanelController.OnAnswerIncorrect += QuitGame;
        BonusChancePanelController.OnTimeout += QuitGame;
    }

    private void OnDisable()
    {
        EnemyObjectController.OnDestroyed -= IncreaseScore;
        SpaceshipController.OnDestroyed -= HandleSpaceshipDestroyed;

        BonusChancePanelController.OnAnswerCorrect -= GiveBonusChance;
        BonusChancePanelController.OnAnswerIncorrect -= QuitGame;
        BonusChancePanelController.OnTimeout -= QuitGame;
    }

    private void IncreaseScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    private void SpawnSpaceship()
    {
        Instantiate(_spaceshipPrefab);
    }

    private void HandleSpaceshipDestroyed()
    {
        if (!_bonusChanceUsed)
        {
            StopCoroutine(_levelProgressionCoroutine);
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
        LoadLevel(_currentLevelIndex);
        _levelProgressionCoroutine = StartCoroutine(LevelProgression(_levelDuration));
        SpawnSpaceship();
    }

    private void LoadLevel(int leveIndex)
    {
        _currentLevelIndex = leveIndex;
        OnLevelChanged?.Invoke(GetCurrentLevelConfig());
        StartCoroutine(InitLevelIndicator(_levels[_currentLevelIndex].levelName));
    }

    private IEnumerator InitLevelIndicator(string levelName)
    {
        _levelIndicator.text = levelName;
        _levelIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _levelIndicator.gameObject.SetActive(false);
    }

    private IEnumerator LevelProgression(float duration)
    {
        while (_currentLevelIndex < _levels.Length)
        {
            yield return new WaitForSeconds(duration);

            // Handle game over
            if (_currentLevelIndex == _levels.Length - 1)
            {
                OnGameOver?.Invoke();
                yield break;
            }

            _currentLevelIndex++;
            LoadLevel(_currentLevelIndex);
        }
    }

    public LevelConfig GetCurrentLevelConfig()
    {
        return _levels[_currentLevelIndex];
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}