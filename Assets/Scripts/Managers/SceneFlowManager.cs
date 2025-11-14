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
        EventBus.Subscribe<GameOverPanelQuitGameButtonClickedEvent>(HandleGameOverPanelQuitGameButtonClicked);
        EventBus.Subscribe<MenuSceneExitButtonClickEvent>(HandleMenuSceneExitButtonClick);
        EventBus.Subscribe<BackToMainMenuEvent>(HandleBackToMainMenuEvent);
        EventBus.Subscribe<PauseMenuQuitGameButtonClickEvent>(HandlePauseMenuQuitGameButtonClicked);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<MainMenuPlayButtonClickEvent>(HandleMainMenuPlayButtonClicked);
        EventBus.Unsubscribe<GameOverPanelPlayAgainButtonClickedEvent>(HandleGameOverPanelPlayAgainButtonClicked);
        EventBus.Unsubscribe<GameOverPanelQuitGameButtonClickedEvent>(HandleGameOverPanelQuitGameButtonClicked);
        EventBus.Unsubscribe<MenuSceneExitButtonClickEvent>(HandleMenuSceneExitButtonClick);
        EventBus.Unsubscribe<BackToMainMenuEvent>(HandleBackToMainMenuEvent);
        EventBus.Unsubscribe<PauseMenuQuitGameButtonClickEvent>(HandlePauseMenuQuitGameButtonClicked);
    }

    private void HandleMainMenuPlayButtonClicked(MainMenuPlayButtonClickEvent e)
    {
        LoadGameScene();
    }

    private void HandleGameOverPanelPlayAgainButtonClicked(GameOverPanelPlayAgainButtonClickedEvent e)
    {
        LoadGameScene();
    }

    private void HandleGameOverPanelQuitGameButtonClicked(GameOverPanelQuitGameButtonClickedEvent e)
    {
        QuitGame();
    }

    private void HandleMenuSceneExitButtonClick(MenuSceneExitButtonClickEvent e)
    {
        QuitGame();
    }

    private void HandleBackToMainMenuEvent(BackToMainMenuEvent e)
    {
        LoadMenuScene();
    }

    private void HandlePauseMenuQuitGameButtonClicked(PauseMenuQuitGameButtonClickEvent e)
    {
        QuitGame();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}