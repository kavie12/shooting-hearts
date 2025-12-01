using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// All the UI interaction buttons in the menu scene
public enum MenuSceneButton
{
    AuthMenuLoginButton,
    AuthMenuSignUpButton,
    AuthMenuExitButton,

    LoginFormBackButton,
    SignUpFormBackButton,
    LeaderboardBackButton,
    InfoPanelCloseButton,
    LoginFormResetPasswordButton,
    ResetPasswordFormBackButton,
    
    MainMenuPlayButton,
    MainMenuLeaderboardButton,
    MainMenuExitButton
}

// Manage the UI components in the menu scene
public class MenuSceneUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _loadingSpinner;
    [SerializeField] private GameObject _btnInfo;

    [Header("UI Components")]
    [SerializeField] private GameObject _authMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _loginForm;
    [SerializeField] private GameObject _signUpForm;
    [SerializeField] private GameObject _leaderboard;
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _playerCard;
    [SerializeField] private GameObject _resetPasswordForm;

    private void Start()
    {
        DisplayLoading();

        EventBus.Publish(new OnTokenAuthenticationRequest());

        _btnInfo.GetComponent<Button>().onClick.AddListener(DisplayInfoPanel);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnMenuSceneButtonClick>(HandleMenuSceneButtonClicked);

        EventBus.Subscribe<OnTokenAuthenticationRequestCompleted>(HandleTokenAuthentication);
        EventBus.Subscribe<OnLoginRequestCompleted>(HandleLoginRequestComplete);
        EventBus.Subscribe<OnSignUpRequestCompleted>(HandleSignUpRequestComplete);
        EventBus.Subscribe<OnLogoutRequestCompleted>(HandleLogoutRequestComplete);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnMenuSceneButtonClick>(HandleMenuSceneButtonClicked);

        EventBus.Unsubscribe<OnTokenAuthenticationRequestCompleted>(HandleTokenAuthentication);
        EventBus.Unsubscribe<OnLoginRequestCompleted>(HandleLoginRequestComplete);
        EventBus.Unsubscribe<OnSignUpRequestCompleted>(HandleSignUpRequestComplete);
        EventBus.Unsubscribe<OnLogoutRequestCompleted>(HandleLogoutRequestComplete);
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
                QuitGame();
                break;

            case MenuSceneButton.LoginFormBackButton:
                DisplayAuthMenu();
                break;

            case MenuSceneButton.SignUpFormBackButton:
                DisplayAuthMenu();
                break;

            case MenuSceneButton.MainMenuPlayButton:
                SceneManager.LoadScene("GameScene");
                break;

            case MenuSceneButton.MainMenuLeaderboardButton:
                DisplayLeaderboard();
                break;

            case MenuSceneButton.MainMenuExitButton:
                QuitGame();
                break;

            case MenuSceneButton.LeaderboardBackButton:
                DisplayMainMenu();
                break;

            case MenuSceneButton.InfoPanelCloseButton:
                DisplayMainMenu();
                break;

            case MenuSceneButton.LoginFormResetPasswordButton:
                DisplayResetPasswordForm();
                break;

            case MenuSceneButton.ResetPasswordFormBackButton:
                DisplayLoginForm();
                break;

            default:
                Debug.Log("Not defined in the switch case.");
                break;
        }
    }

    #region Display Component Functions

    private void DisplayLoading()
    {
        _title.SetActive(true);
        _loadingSpinner.SetActive(true);
        _btnInfo.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
        _infoPanel.SetActive(false);
        _playerCard.SetActive(false);
        _resetPasswordForm.SetActive(false);
    }

    private void DisplayAuthMenu()
    {
        _title.SetActive(true);
        _loadingSpinner.SetActive(false);
        _btnInfo.SetActive(false);
        _authMenu.SetActive(true);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
        _infoPanel.SetActive(false);
        _playerCard.SetActive(false);
        _resetPasswordForm.SetActive(false);
    }

    private void DisplayLoginForm()
    {
        _title.SetActive(false);
        _loadingSpinner.SetActive(false);
        _btnInfo.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(true);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
        _infoPanel.SetActive(false);
        _playerCard.SetActive(false);
        _resetPasswordForm.SetActive(false);
    }

    private void DisplaySignUpForm()
    {
        _title.SetActive(false);
        _loadingSpinner.SetActive(false);
        _btnInfo.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(true);
        _leaderboard.SetActive(false);
        _infoPanel.SetActive(false);
        _playerCard.SetActive(false);
        _resetPasswordForm.SetActive(false);
    }

    private void DisplayMainMenu()
    {
        _title.SetActive(true);
        _loadingSpinner.SetActive(false);
        _btnInfo.SetActive(true);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
        _infoPanel.SetActive(false);
        _playerCard.SetActive(true);
        _resetPasswordForm.SetActive(false);
    }

    private void DisplayLeaderboard()
    {
        _title.SetActive(false);
        _loadingSpinner.SetActive(false);
        _btnInfo.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(true);
        _infoPanel.SetActive(false);
        _playerCard.SetActive(true);
        _resetPasswordForm.SetActive(false);
    }

    private void DisplayInfoPanel()
    {
        _title.SetActive(true);
        _loadingSpinner.SetActive(false);
        _btnInfo.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
        _infoPanel.SetActive(true);
        _playerCard.SetActive(true);
        _resetPasswordForm.SetActive(false);
    }

    private void DisplayResetPasswordForm()
    {
        _title.SetActive(false);
        _loadingSpinner.SetActive(false);
        _btnInfo.SetActive(false);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(false);
        _loginForm.SetActive(false);
        _signUpForm.SetActive(false);
        _leaderboard.SetActive(false);
        _infoPanel.SetActive(false);
        _playerCard.SetActive(false);
        _resetPasswordForm.SetActive(true);
    }

    #endregion

    #region Auth Functions

    private void HandleTokenAuthentication(OnTokenAuthenticationRequestCompleted e)
    {
        if (e.Success)
        {
            DisplayMainMenu();
        }
        else
        {
            DisplayAuthMenu();
        }
    }

    private void HandleLoginRequestComplete(OnLoginRequestCompleted e)
    {
        if (e.Success) Invoke(nameof(OnFormLoginSuccess), 1f);
    }

    private void OnFormLoginSuccess()
    {
        DisplayMainMenu();
    }

    private void HandleSignUpRequestComplete(OnSignUpRequestCompleted e)
    {
        if (e.Success) Invoke(nameof(OnFormSignUpSuccess), 1f);
    }

    private void OnFormSignUpSuccess()
    {
        DisplayMainMenu();
    }

    private void HandleLogoutRequestComplete(OnLogoutRequestCompleted e)
    {
        DisplayAuthMenu();
    }

    #endregion

    private void QuitGame()
    {
        Application.Quit();
    }
}