using UnityEngine;

// Manage game pause functionality, including pausing and resuming the game based on user input.
public class PauseManager : MonoBehaviour
{
    private bool _paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBus.Publish(new OnGamePaused(!_paused));
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnGamePaused>(HandleGamePaused);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnGamePaused>(HandleGamePaused);
    }

    private void HandleGamePaused(OnGamePaused e)
    {
        _paused = e.Paused;

        if (_paused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}