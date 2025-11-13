using UnityEngine;

public class GameSceneUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _bonusChancePanel;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        EventBus.Subscribe<BonusChancePanelActivateEvent>(HandleBonusChancePanelActivate);
        EventBus.Subscribe<GamePauseEvent>(HandleGamePause);
        EventBus.Subscribe<GameOverEvent>(HandleGameOver);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<BonusChancePanelActivateEvent>(HandleBonusChancePanelActivate);
        EventBus.Unsubscribe<GamePauseEvent>(HandleGamePause);
        EventBus.Unsubscribe<GameOverEvent>(HandleGameOver);
    }

    private void HandleBonusChancePanelActivate(BonusChancePanelActivateEvent e)
    {
        _bonusChancePanel.SetActive(true);
    }

    private void HandleGamePause(GamePauseEvent e)
    {
        if (e.IsPaused) _pauseMenu.SetActive(true);
        else if (_pauseMenu.activeSelf) _pauseMenu.SetActive(false);
    }

    private void HandleGameOver(GameOverEvent e)
    {
        _gameOverPanel.SetActive(true);
    }
}