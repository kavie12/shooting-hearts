using System;
using UnityEngine;

public class GameSceneUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelIndicator;
    [SerializeField] private GameObject _bonusChancePanel;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        EventBus.Subscribe<LevelStartedEvent>(HandleLevelStarted);
        EventBus.Subscribe<BonusChancePanelActivateEvent>(HandleBonusChancePanelActivate);
        EventBus.Subscribe<PauseMenuToggleEvent>(HandlePauseMenuToggle);
        EventBus.Subscribe<GameOverEvent>(HandleGameOver);
        EventBus.Subscribe<GameOverPanelMainMenuButtonClickedEvent>(HandleGameOverPanelMainMenuButtonClick);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelStartedEvent>(HandleLevelStarted);
        EventBus.Unsubscribe<BonusChancePanelActivateEvent>(HandleBonusChancePanelActivate);
        EventBus.Unsubscribe<PauseMenuToggleEvent>(HandlePauseMenuToggle);
        EventBus.Unsubscribe<GameOverEvent>(HandleGameOver);
        EventBus.Unsubscribe<GameOverPanelMainMenuButtonClickedEvent>(HandleGameOverPanelMainMenuButtonClick);
    }

    private void HandleLevelStarted(LevelStartedEvent e)
    {
        _levelIndicator.SetActive(true);
        _levelIndicator.GetComponent<LevelIndicator>().DisplayLevelName(e.LevelConfig.LevelName);
    }

    private void HandleBonusChancePanelActivate(BonusChancePanelActivateEvent e)
    {
        _bonusChancePanel.SetActive(true);
    }

    private void HandlePauseMenuToggle(PauseMenuToggleEvent e)
    {
        if (e.Paused) _pauseMenu.SetActive(true);
        else if (_pauseMenu.activeSelf) _pauseMenu.SetActive(false);
    }

    private void HandleGameOver(GameOverEvent e)
    {
        _gameOverPanel.SetActive(true);
    }

    private void HandleGameOverPanelMainMenuButtonClick(GameOverPanelMainMenuButtonClickedEvent e)
    {
        EventBus.Publish(new BackToMainMenuEvent());
    }
}