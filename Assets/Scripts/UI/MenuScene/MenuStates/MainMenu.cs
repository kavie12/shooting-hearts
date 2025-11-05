using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenuState
{
    public static event Action OnPlayClicked;
    public static event Action OnLogoutClicked;
    public static event Action OnExitClicked;

    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnLeaderboard;
    [SerializeField] private Button _btnLogout;
    [SerializeField] private Button _btnExit;

    void Start()
    {
        _btnPlay.onClick.AddListener(() => OnPlayClicked?.Invoke());
        _btnLeaderboard.onClick.AddListener(() => StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.LEADERBOARD)));
        _btnLogout.onClick.AddListener(HandleLogout);
        _btnExit.onClick.AddListener(() => OnExitClicked?.Invoke());
    }

    void HandleLogout()
    {
        OnLogoutClicked?.Invoke();
        StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.AUTH_MENU));
    }
}
