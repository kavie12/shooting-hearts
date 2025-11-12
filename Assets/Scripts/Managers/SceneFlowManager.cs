using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    private static SceneFlowManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<MainMenuPlayButtonClickEvent>(HandleMainMenuPlayButtonClicked);
        EventBus.Subscribe<GameOverPanelPlayAgainButtonClickedEvent>(HandleGameOverPanelPlayAgainButtonClicked);
        EventBus.Subscribe<GameOverPanelMainMenuButtonClickedEvent>(HandleGameOverPanelMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<MainMenuPlayButtonClickEvent>(HandleMainMenuPlayButtonClicked);
        EventBus.Unsubscribe<GameOverPanelPlayAgainButtonClickedEvent>(HandleGameOverPanelPlayAgainButtonClicked);
        EventBus.Unsubscribe<GameOverPanelMainMenuButtonClickedEvent>(HandleGameOverPanelMainMenuButtonClicked);
    }

    private void HandleMainMenuPlayButtonClicked(MainMenuPlayButtonClickEvent e)
    {
        SceneManager.LoadScene("GameScene");
    }

    private void HandleGameOverPanelPlayAgainButtonClicked(GameOverPanelPlayAgainButtonClickedEvent e)
    {
        SceneManager.LoadScene("GameScene");
    }

    private void HandleGameOverPanelMainMenuButtonClicked(GameOverPanelMainMenuButtonClickedEvent e)
    {
        SceneManager.LoadScene("MenuScene");
    }
}