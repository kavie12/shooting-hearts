using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTextMeshProObject;
    [SerializeField] private GameObject _spaceshipPrefab;

    private int _score = 0;

    private void Start()    
    {
        SpawnSpaceship();
        _scoreTextMeshProObject.text = _score.ToString();
    }

    private void OnEnable()
    {
        EnemyObjectController.OnDestroyed += IncreaseScore;
        BonusChancePanelController.OnAnswerCorrect += SpawnSpaceship;
    }

    private void OnDisable()
    {
        EnemyObjectController.OnDestroyed -= IncreaseScore;
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
}