using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnLeaderboard;
    [SerializeField] private Button _btnExit;

    private void Start()
    {
        _btnPlay.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.MainMenuPlayButton)));
        _btnLeaderboard.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.MainMenuLeaderboardButton)));
        _btnExit.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.MainMenuExitButton)));
    }
}