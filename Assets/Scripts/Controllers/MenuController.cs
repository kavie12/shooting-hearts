using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static event Action<LoginRequest> OnLoginRequest;
    public static event Action<SignUpRequest> OnSignUpRequest;

    [SerializeField] private GameObject _menuContainer;

    [Header("Auth Menu")]
    [SerializeField] private GameObject _authMenu;
    [SerializeField] private Button _btnLogin;
    [SerializeField] private Button _btnSignUp;
    [SerializeField] private Button _btnExit1;

    [Header("Main Menu")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnLeaderboard;
    [SerializeField] private Button _btnLogout;
    [SerializeField] private Button _btnExit2;

    [Header("Login Form")]
    [SerializeField] private GameObject _loginForm;
    [SerializeField] private TMP_InputField _loginFormEmail;
    [SerializeField] private TMP_InputField _loginFormPassword;
    [SerializeField] private Button _loginFormBtnLogin;
    [SerializeField] private Button _loginFormBtnBack;

    [Header("Sign Up Form")]
    [SerializeField] private GameObject _signUpForm;
    [SerializeField] private TMP_InputField _signUpFormName;
    [SerializeField] private TMP_InputField _signUpFormEmail;
    [SerializeField] private TMP_InputField _signUpFormPassword;
    [SerializeField] private Button _signUpFormBtnSignUp;
    [SerializeField] private Button _signUpFormBtnBack;

    [Header("Leaderboard")]
    [SerializeField] private GameObject _leaderboard;
    [SerializeField] private Button _leaderboardBtnBack;

    private void OnEnable()
    {
        AuthManager.OnSignUpSuccess += HandleOnSignUpSuccess;
        AuthManager.OnLoginSuccess += HandleOnLoginSuccess;
    }

    private void OnDisable()
    {
        AuthManager.OnSignUpSuccess -= HandleOnSignUpSuccess;
        AuthManager.OnLoginSuccess -= HandleOnLoginSuccess;
    }

    void Start()
    {
        _btnLogin.onClick.AddListener(() => {
            _menuContainer.SetActive(false);
            _loginForm.SetActive(true);
        });
        _loginFormBtnBack.onClick.AddListener(() => {
            _menuContainer.SetActive(true);
            _loginForm.SetActive(false);
        });
        _btnSignUp.onClick.AddListener(() => {
            _menuContainer.SetActive(false);
            _signUpForm.SetActive(true);
        });
        _signUpFormBtnBack.onClick.AddListener(() => {
            _menuContainer.SetActive(true);
            _signUpForm.SetActive(false);
        });
        _btnExit1.onClick.AddListener(QuitGame);

        _loginFormBtnLogin.onClick.AddListener(() => HandleLogin(_loginFormEmail.text, _loginFormPassword.text));
        _signUpFormBtnSignUp.onClick.AddListener(() => HandleSignUp(_signUpFormName.text, _signUpFormEmail.text, _signUpFormPassword.text));

        _btnLeaderboard.onClick.AddListener(() => {
            _mainMenu.SetActive(false);
            _leaderboard.SetActive(true);
        });
        _leaderboardBtnBack.onClick.AddListener(() => {
            _mainMenu.SetActive(true);
            _leaderboard.SetActive(false);
        });

        _btnLogout.onClick.AddListener(HandleLogOut);
        _btnExit2.onClick.AddListener(QuitGame);
    }

    void HandleSignUp(string name, string email, string password)
    {
        OnSignUpRequest?.Invoke(new SignUpRequest(name, email, password));
    }

    void HandleLogin(string email, string password)
    {
        OnLoginRequest?.Invoke(new LoginRequest(email, password));
    }

    void HandleOnSignUpSuccess()
    {
        _signUpForm.SetActive(false);
        _menuContainer.SetActive(true);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    void HandleOnLoginSuccess()
    {
        _loginForm.SetActive(false);
        _menuContainer.SetActive(true);
        _authMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    void HandleLogOut()
    {
        _mainMenu.SetActive(false);
        _authMenu.SetActive(true);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
