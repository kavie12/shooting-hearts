using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginForm : BaseMenuState
{
    public static event Action<LoginRequest> OnLoginRequest;

    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Button _btnLogin;
    [SerializeField] private Button _btnBack;
    [SerializeField] private TextMeshProUGUI _message;

    private void OnEnable()
    {
        AuthManager.OnLoginSuccess += HandleOnLoginSuccess;
        AuthManager.OnLoginFailed += HandleOnLoginFailed;
    }

    private void OnDisable()
    {
        AuthManager.OnLoginSuccess -= HandleOnLoginSuccess;
        AuthManager.OnLoginFailed -= HandleOnLoginFailed;
        
        ResetForm();
    }

    void Start()
    {
        _btnLogin.onClick.AddListener(HandleLogin);
        _btnBack.onClick.AddListener(() => StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.AUTH_MENU)));
    }

    void HandleLogin()
    {
        _btnLogin.interactable = false;
        OnLoginRequest?.Invoke(new LoginRequest(_email.text, _password.text));
    }

    void HandleOnLoginSuccess(string message)
    {
        _message.text = message;
        _message.color = Color.green;
        _message.gameObject.SetActive(true);

        _btnBack.interactable = false;

        StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.MAIN_MENU, 1f));
    }

    void HandleOnLoginFailed(string message)
    {
        _message.text = message;
        _message.color = Color.red;
        _message.gameObject.SetActive(true);
        _btnLogin.interactable = true;
    }

    void ResetForm()
    {
        _email.text = "";
        _password.text = "";
        _btnLogin.interactable = true;
        _btnBack.interactable = true;
        _message.gameObject.SetActive(false);
    }
}