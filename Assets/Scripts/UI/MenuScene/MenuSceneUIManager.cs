using System.Collections;
using UnityEngine;

public class MenuSceneUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _title;

    [Header("UI Components")]
    [SerializeField] private GameObject _authMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _loginForm;
    [SerializeField] private GameObject _signUpForm;
    [SerializeField] private GameObject _leaderboard;

    private void Start()
    {
        EventBus.Publish(new MenuSceneLoaded());
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnAuthMenuVerifyTokenSuccessEvent>(HandleAuthMenuVerifyTokenSuccess);

        EventBus.Subscribe<AuthMenuLoginButtonClickEvent>(HandleAuthMenuLoginButtonClick);
        EventBus.Subscribe<AuthMenuSignUpButtonClickEvent>(HandleAuthMenuSignUpButtonClick);

        EventBus.Subscribe<LoginFormBackButtonClickEvent>(HandleLoginFormBackButtonClick);
        EventBus.Subscribe<SignUpFormBackButtonClickEvent>(HandleSignUpFormBackButtonClick);

        EventBus.Subscribe<MainMenuLeaderboardButtonClickEvent>(HandleMainMenuLeaderboardButtonClick);
        EventBus.Subscribe<LeaderboardBackButtonClickEvent>(HandleLeaderboardBackButtonClick);

        EventBus.Subscribe<MenuSceneExitButtonClickEvent>(HandleMenuSceneExitButtonClick);

        EventBus.Subscribe<OnLoginSuccessEvent>(HandleOnLoginSuccess);
        EventBus.Subscribe<OnSignUpSuccessEvent>(HandleOnSignUpSuccess);
        EventBus.Subscribe<OnLogoutSuccessEvent>(HandleOnLogoutSuccess);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnAuthMenuVerifyTokenSuccessEvent>(HandleAuthMenuVerifyTokenSuccess);

        EventBus.Unsubscribe<AuthMenuLoginButtonClickEvent>(HandleAuthMenuLoginButtonClick);
        EventBus.Unsubscribe<AuthMenuSignUpButtonClickEvent>(HandleAuthMenuSignUpButtonClick);

        EventBus.Unsubscribe<LoginFormBackButtonClickEvent>(HandleLoginFormBackButtonClick);
        EventBus.Unsubscribe<SignUpFormBackButtonClickEvent>(HandleSignUpFormBackButtonClick);

        EventBus.Unsubscribe<MainMenuLeaderboardButtonClickEvent>(HandleMainMenuLeaderboardButtonClick);
        EventBus.Unsubscribe<LeaderboardBackButtonClickEvent>(HandleLeaderboardBackButtonClick);

        EventBus.Unsubscribe<MenuSceneExitButtonClickEvent>(HandleMenuSceneExitButtonClick);

        EventBus.Unsubscribe<OnLoginSuccessEvent>(HandleOnLoginSuccess);
        EventBus.Unsubscribe<OnSignUpSuccessEvent>(HandleOnSignUpSuccess);
        EventBus.Unsubscribe<OnLogoutSuccessEvent>(HandleOnLogoutSuccess);
    }

    private void HandleAuthMenuVerifyTokenSuccess(OnAuthMenuVerifyTokenSuccessEvent e)
    {
        _authMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    private void HandleAuthMenuLoginButtonClick(AuthMenuLoginButtonClickEvent e)
    {
        _title.SetActive(false);
        _authMenu.SetActive(false);
        _loginForm.SetActive(true);
    }

    private void HandleAuthMenuSignUpButtonClick(AuthMenuSignUpButtonClickEvent e)
    {
        _title.SetActive(false);
        _authMenu.SetActive(false);
        _signUpForm.SetActive(true);
    }

    private void HandleLoginFormBackButtonClick(LoginFormBackButtonClickEvent e)
    {
        _loginForm.SetActive(false);
        _title.SetActive(true);
        _authMenu.SetActive(true);
    }

    private void HandleSignUpFormBackButtonClick(SignUpFormBackButtonClickEvent e)
    {
        _signUpForm.SetActive(false);
        _title.SetActive(true);
        _authMenu.SetActive(true);
    }

    private void HandleMainMenuLeaderboardButtonClick(MainMenuLeaderboardButtonClickEvent e)
    {
        _title.SetActive(false);
        _mainMenu.SetActive(false);
        _leaderboard.SetActive(true);
    }

    private void HandleMenuSceneExitButtonClick(MenuSceneExitButtonClickEvent e)
    {
        Application.Quit();
    }

    private void HandleLeaderboardBackButtonClick(LeaderboardBackButtonClickEvent e)
    {
        _leaderboard.SetActive(false);
        _title.SetActive(true);
        _mainMenu.SetActive(true);
    }

    private void HandleOnLoginSuccess(OnLoginSuccessEvent e)
    {
        StartCoroutine(OnFormLoginSuccess());
    }

    private IEnumerator OnFormLoginSuccess()
    {
        yield return new WaitForSeconds(1f);
        _loginForm.SetActive(false);
        _title.SetActive(true);
        _mainMenu.SetActive(true);
    }

    private void HandleOnSignUpSuccess(OnSignUpSuccessEvent e)
    {
        StartCoroutine(OnFormSignUpSuccess());
    }

    private IEnumerator OnFormSignUpSuccess()
    {
        yield return new WaitForSeconds(1f);
        _signUpForm.SetActive(false);
        _title.SetActive(true);
        _mainMenu.SetActive(true);
    }

    private void HandleOnLogoutSuccess(OnLogoutSuccessEvent e)
    {
        _mainMenu.SetActive(false);
        _title.SetActive(true);
        _authMenu.SetActive(true);
    }
}