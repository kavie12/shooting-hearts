using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool _isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause(!_isPaused);
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<PauseMenuResumeButtonClickEvent>(HandlePauseMenuResumeButtonClick);
        EventBus.Subscribe<PauseMenuMainMenuButtonClickEvent>(HandlePauseMenuMainMenuButtonClick);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PauseMenuResumeButtonClickEvent>(HandlePauseMenuResumeButtonClick);
        EventBus.Unsubscribe<PauseMenuMainMenuButtonClickEvent>(HandlePauseMenuMainMenuButtonClick);
    }

    private void HandlePauseMenuResumeButtonClick(PauseMenuResumeButtonClickEvent e)
    {
        HandlePause(false);
    }

    private void HandlePauseMenuMainMenuButtonClick(PauseMenuMainMenuButtonClickEvent e)
    {
        HandlePause(false);
        EventBus.Publish(new BackToMainMenuEvent());
    }

    private void HandlePause(bool pause)
    {
        _isPaused = pause;

        if (_isPaused)
        {
            Time.timeScale = 0f;
            EventBus.Publish(new GamePauseEvent());
        }
        else
        {
            Time.timeScale = 1f;
            EventBus.Publish(new GameResumeEvent());
        }

        EventBus.Publish(new PauseMenuToggleEvent(_isPaused));
    }
}