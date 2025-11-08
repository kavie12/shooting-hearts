using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        EventBus.Subscribe<OnGameOverPanelPlayAgainButtonClickedEvent>(HandlePlayAgain);
        EventBus.Subscribe<OnGameOverPanelMainMenuButtonClickedEvent>(HandleMainMenu);
        EventBus.Subscribe<OnFinalScoresReadyEvent>(DisplayGameOverPanel);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverPanelPlayAgainButtonClickedEvent>(HandlePlayAgain);
        EventBus.Unsubscribe<OnGameOverPanelMainMenuButtonClickedEvent>(HandleMainMenu);
        EventBus.Unsubscribe<OnFinalScoresReadyEvent>(DisplayGameOverPanel);
    }

    private void DisplayGameOverPanel(OnFinalScoresReadyEvent e)
    {
        _gameOverPanel.SetActive(true);
    }

    private void HandlePlayAgain(OnGameOverPanelPlayAgainButtonClickedEvent e)
    {
        SceneManager.LoadScene("GameScene");
    }

    private void HandleMainMenu(OnGameOverPanelMainMenuButtonClickedEvent e)
    {
        SceneManager.LoadScene("MenuScene");
    }
}