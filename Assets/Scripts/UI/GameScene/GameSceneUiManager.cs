using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelIndicator;
    [SerializeField] private GameObject _bonusChancePanel;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        EventBus.Subscribe<OnLevelStarted>(HandleOnLevelStarted);
        EventBus.Subscribe<OnBonusChanceRequested>(HandleBonusChanceRequest);
        EventBus.Subscribe<OnGameOver>(HandleGameOver);
        EventBus.Subscribe<OnGameOverPanelButtonClicked>(HandleGameOverPanelButtonClicked);
        EventBus.Subscribe<OnGamePaused>(HandleGamePaused);
        EventBus.Subscribe<OnPauseMenuButtonClicked>(HandlePauseMenuButtonClicked);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnLevelStarted>(HandleOnLevelStarted);
        EventBus.Unsubscribe<OnBonusChanceRequested>(HandleBonusChanceRequest);
        EventBus.Unsubscribe<OnGameOver>(HandleGameOver);
        EventBus.Unsubscribe<OnGameOverPanelButtonClicked>(HandleGameOverPanelButtonClicked);
        EventBus.Unsubscribe<OnGamePaused>(HandleGamePaused);
        EventBus.Unsubscribe<OnPauseMenuButtonClicked>(HandlePauseMenuButtonClicked);
    }

    private void HandleOnLevelStarted(OnLevelStarted e)
    {
        _levelIndicator.SetActive(true);
        _levelIndicator.GetComponent<LevelIndicator>().Display(e.LevelName);
    }

    private void HandleBonusChanceRequest(OnBonusChanceRequested e)
    {
        _bonusChancePanel.SetActive(true);
    }

    private void HandleGameOver(OnGameOver e)
    {
        _gameOverPanel.SetActive(true);
        _gameOverPanel.GetComponent<GameOverPanel>().InitGameOverPanelText(e.Win);
    }

    private void HandleGameOverPanelButtonClicked(OnGameOverPanelButtonClicked e)
    {
        switch (e.ButtonId)
        {
            case GameOverPanelButton.PlayAgainButton:
                SceneManager.LoadScene("GameScene");
                break;

            case GameOverPanelButton.MainMenuButton:
                SceneManager.LoadScene("MenuScene");
                break;

            default:
                Debug.Log("Not defined in the switch case.");
                break;
        }
    }

    private void HandleGamePaused(OnGamePaused e)
    {
        if (e.Paused) _pauseMenu.SetActive(true);
        else _pauseMenu.SetActive(false);
    }

    private void HandlePauseMenuButtonClicked(OnPauseMenuButtonClicked e)
    {
        switch (e.ButtonId)
        {
            case PauseMenuButton.ResumeButton:
                EventBus.Publish(new OnGamePaused(false));
                break;

            case PauseMenuButton.MainMenuButton:
                EventBus.Publish(new OnGamePaused(false));
                SceneManager.LoadScene("MenuScene");
                break;

            default:
                Debug.Log("Not defined in the switch case.");
                break;
        }
    }
}