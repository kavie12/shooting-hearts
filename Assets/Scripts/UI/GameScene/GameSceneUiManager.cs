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
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnLevelStarted>(HandleOnLevelStarted);
        EventBus.Unsubscribe<OnBonusChanceRequested>(HandleBonusChanceRequest);
        EventBus.Unsubscribe<OnGameOver>(HandleGameOver);
        EventBus.Unsubscribe<OnGameOverPanelButtonClicked>(HandleGameOverPanelButtonClicked);
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
    }

    private void HandleGameOverPanelButtonClicked(OnGameOverPanelButtonClicked e)
    {
        switch (e.ButtonId)
        {
            case GameOverPanelButton.PlayAgainButton:
                SceneManager.LoadScene("GameScene");
                break;

            case GameOverPanelButton.MainMenuButton:
                SceneManager.LoadScene("GameScene");
                break;

            default:
                Debug.Log("Not defined in the switch case.");
                break;
        }
    }
}