using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuSceneButton
{
    AuthMenuLoginButton,
    AuthMenuSignUpButton,
    AuthMenuExitButton,
    LoginFormBackButton,
    SignUpFormBackButton,
    MainMenuPlayButton,
    MainMenuLeaderboardButton,
    MainMenuExitButton,
    LeaderboardBackButton
}

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
        EventBus.Publish(new OnTokenAuthenticationRequest());
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnMenuSceneButtonClick>(HandleMenuSceneButtonClicked);

        EventBus.Subscribe<OnTokenAuthenticationRequestComplete>(HandleTokenAuthentication);
        EventBus.Subscribe<OnLoginRequestComplete>(HandleLoginRequestComplete);
        EventBus.Subscribe<OnSignUpRequestComplete>(HandleSignUpRequestComplete);
        EventBus.Subscribe<OnLogoutRequestComplete>(HandleLogoutRequestComplete);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnMenuSceneButtonClick>(HandleMenuSceneButtonClicked);

        EventBus.Unsubscribe<OnTokenAuthenticationRequestComplete>(HandleTokenAuthentication);
        EventBus.Unsubscribe<OnLoginRequestComplete>(HandleLoginRequestComplete);
        EventBus.Unsubscribe<OnSignUpRequestComplete>(HandleSignUpRequestComplete);
        EventBus.Unsubscribe<OnLogoutRequestComplete>(HandleLogoutRequestComplete);
    }

    private void HandleMenuSceneButtonClicked(OnMenuSceneButtonClick e)
    {
        switch (e.ButtonId)
        {
            case MenuSceneButton.AuthMenuLoginButton:
                DisplayLoginForm();
                break;

            case MenuSceneButton.AuthMenuSignUpButton:
                DisplaySignUpForm();
                break;

            case MenuSceneButton.AuthMenuExitButton:
                break;

            case MenuSceneButton.LoginFormBackButton:
                DisplayAuthMenu();
                break;

            case MenuSceneButton.SignUpFormBackButton:
                DisplaySignUpForm();
                break;

            case MenuSceneButton.MainMenuPlayButton:
                SceneManager.LoadScene("GameScene");
                break;

            case MenuSceneButton.MainMenuLeaderboardButton:
                DisplayLeaderboard();
                break;

            case MenuSceneButton.MainMenuExitButton:
                break;

            case MenuSceneButton.LeaderboardBackButton:
                DisplayMainMenu();
                break;

            default:
                Debug.Log("Not defined in the switch case.");
                break;
        }
    }

    #region Display Component Functions

    private void DisplayAuthMenu()
    {
        _title.SetActive(true);
        _authMenu.SetActive(true);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
    }

    private void DisplayLoginForm()
    {
        _title.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(true);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
    }

    private void DisplaySignUpForm()
    {
        _title.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(true);
        _leaderboard.SetActive(false);
    }

    private void DisplayMainMenu()
    {
        _title.SetActive(true);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
    }

    private void DisplayLeaderboard()
    {
        _title.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(true);
    }

    #endregion

    #region Auth Functions

    private void HandleTokenAuthentication(OnTokenAuthenticationRequestComplete e)
    {
        if (e.Success)
        {
            DisplayMainMenu();
        }
    }

    private void HandleLoginRequestComplete(OnLoginRequestComplete e)
    {
        if (e.Success) Invoke(nameof(OnFormLoginSuccess), 1f);
    }

    private void OnFormLoginSuccess()
    {
        DisplayMainMenu();
    }

    private void HandleSignUpRequestComplete(OnSignUpRequestComplete e)
    {
        if (e.Success) Invoke(nameof(OnFormSignUpSuccess), 1f);
    }

    private void OnFormSignUpSuccess()
    {
        DisplayMainMenu();
    }

    private void HandleLogoutRequestComplete(OnLogoutRequestComplete e)
    {
        DisplayAuthMenu();
    }

    #endregion
}