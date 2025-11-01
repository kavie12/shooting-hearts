using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTextMeshProObject;
    [SerializeField] private GameObject _spaceshipPrefab;
    [SerializeField] private GameObject _bonusChancePanel;

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
        SpaceshipController.OnDestroyed += HandleSpaceshipDestroyed;

        BonusChancePanelController.OnAnswerCorrect += SpawnSpaceship;
        BonusChancePanelController.OnAnswerIncorrect += QuitGame;
    }

    private void OnDisable()
    {
        EnemyObjectController.OnDestroyed -= IncreaseScore;
        SpaceshipController.OnDestroyed -= HandleSpaceshipDestroyed;

        BonusChancePanelController.OnAnswerCorrect -= SpawnSpaceship;
        BonusChancePanelController.OnAnswerIncorrect -= QuitGame;
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
            Invoke("ShowBonusChanceQuestionPanel", 2f);
            _bonusChanceUsed = true;
        }
        else
        {
            Invoke("QuitGame", 3f);
        }
    }

    private void ShowBonusChanceQuestionPanel()
    {
        _bonusChancePanel.SetActive(true);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}