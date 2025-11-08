using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<MainMenuPlayButtonClickEvent>(HandleMainMenuPlayButtonClicked);
        EventBus.Subscribe<OnGameOverPanelPlayAgainButtonClickedEvent>(HandleGameOverPanelPlayAgainButtonClicked);
        EventBus.Subscribe<OnGameOverPanelMainMenuButtonClickedEvent>(HandleGameOverPanelMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<MainMenuPlayButtonClickEvent>(HandleMainMenuPlayButtonClicked);
        EventBus.Unsubscribe<OnGameOverPanelPlayAgainButtonClickedEvent>(HandleGameOverPanelPlayAgainButtonClicked);
        EventBus.Unsubscribe<OnGameOverPanelMainMenuButtonClickedEvent>(HandleGameOverPanelMainMenuButtonClicked);
    }

    private void HandleMainMenuPlayButtonClicked(MainMenuPlayButtonClickEvent e)
    {
        SceneManager.LoadScene("GameScene");
    }

    private void HandleGameOverPanelPlayAgainButtonClicked(OnGameOverPanelPlayAgainButtonClickedEvent e)
    {
        SceneManager.LoadScene("GameScene");
    }

    private void HandleGameOverPanelMainMenuButtonClicked(OnGameOverPanelMainMenuButtonClickedEvent e)
    {
        SceneManager.LoadScene("MenuScene");
    }
}