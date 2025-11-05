using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuSceneState
{
    AUTH_MENU,
    MAIN_MENU,
    LOGIN_FORM,
    SIGN_UP_FORM,
    LEADERBOARD
}

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _title;

    [Header("UI States")]
    [SerializeField] private AuthMenu _authMenu;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LoginForm _loginForm;
    [SerializeField] private SignUpForm _signUpForm;
    [SerializeField] private Leaderboard _leaderboard;

    private Dictionary<MenuSceneState, BaseMenuState> _states;

    private void OnEnable()
    {
        AuthMenu.OnExitClicked += QuitGame;
        MainMenu.OnExitClicked += QuitGame;
        MainMenu.OnPlayClicked += LoadGameScene;
    }

    private void OnDisable()
    {
        AuthMenu.OnExitClicked -= QuitGame;
        MainMenu.OnExitClicked -= QuitGame;
        MainMenu.OnPlayClicked -= LoadGameScene;
    }

    private void Awake()
    {
        _states = new Dictionary<MenuSceneState, BaseMenuState>
        {
            { MenuSceneState.AUTH_MENU, _authMenu },
            { MenuSceneState.MAIN_MENU, _mainMenu },
            { MenuSceneState.LOGIN_FORM, _loginForm },
            { MenuSceneState.SIGN_UP_FORM, _signUpForm },
            { MenuSceneState.LEADERBOARD, _leaderboard },
        };

        foreach (KeyValuePair<MenuSceneState, BaseMenuState> state in _states)
        { 
            state.Value.Hide();
        }

        _title.SetActive(false);
        StartCoroutine(ChangeState(MenuSceneState.AUTH_MENU));
    }

    public IEnumerator ChangeState(MenuSceneState stateToChange, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (KeyValuePair<MenuSceneState, BaseMenuState> state in _states)
        {
            state.Value.gameObject.SetActive(state.Key == stateToChange);

            _title.SetActive(stateToChange == MenuSceneState.MAIN_MENU || stateToChange == MenuSceneState.AUTH_MENU);
        }
    }

    public IEnumerator ChangeState(MenuSceneState stateToChange)
    {
        yield return ChangeState(stateToChange, 0f);
    }

    void LoadGameScene()
    {
        CustomSceneManager.ChangeScene(Scene.GAME_SCENE);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}