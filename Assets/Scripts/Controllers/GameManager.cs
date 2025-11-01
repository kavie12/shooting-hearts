using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static event Action OnUseBonusChance;

    [SerializeField] private TextMeshProUGUI _scoreTextMeshProObject;
    [SerializeField] private GameObject _spaceshipPrefab;

    private int _score = 0;

    private bool _bonusChanceUsed = false;

    private void Start()    
    {
        SpawnSpaceship();
        _scoreTextMeshProObject.text = _score.ToString();
    }

    private void OnEnable()
    {
        EnemyObjectController.OnDestroyed += IncreaseScore;
        BonusChancePanelController.OnAnswerCorrect += SpawnSpaceship;
        SpaceshipController.OnDestroyed += HandleSpaceshipDestroyed;
    }

    private void OnDisable()
    {
        EnemyObjectController.OnDestroyed -= IncreaseScore;
        BonusChancePanelController.OnAnswerCorrect += SpawnSpaceship;
        SpaceshipController.OnDestroyed -= HandleSpaceshipDestroyed;
    }

    private void IncreaseScore(int score)
    {
        _score += score;
        _scoreTextMeshProObject.text = _score.ToString();
    }

    private void SpawnSpaceship()
    {
        Instantiate(_spaceshipPrefab);
    }

    private void HandleSpaceshipDestroyed()
    {
        if (!_bonusChanceUsed)
        {
            OnUseBonusChance?.Invoke();
            _bonusChanceUsed = true;
        }
        else
        {
            Invoke("QuiteGame", 3f);
            
        }
    }

    private void QuiteGame()
    {
        Application.Quit();
    }
}