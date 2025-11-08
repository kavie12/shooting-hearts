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
        EventBus.Subscribe<MainMenuPlayButtonClickEvent>(HandlePlayButtonClicked);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<MainMenuPlayButtonClickEvent>(HandlePlayButtonClicked);
    }

    private void HandlePlayButtonClicked(MainMenuPlayButtonClickEvent e)
    {
        SceneManager.LoadScene("GameScene");
    }
}