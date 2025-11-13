using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnLeaderboard;
    [SerializeField] private Button _btnLogout;
    [SerializeField] private Button _btnExit;

    private void Start()
    {
        _btnPlay.onClick.AddListener(() => EventBus.Publish(new MainMenuPlayButtonClickEvent()));
        _btnLeaderboard.onClick.AddListener(() => EventBus.Publish(new MainMenuLeaderboardButtonClickEvent()));
        _btnLogout.onClick.AddListener(() => EventBus.Publish(new MainMenuLogoutButtonClickEvent()));
        _btnExit.onClick.AddListener(() => EventBus.Publish(new MenuSceneExitButtonClickEvent()));
    }
}
