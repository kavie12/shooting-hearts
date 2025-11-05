using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignUpForm : BaseMenuState
{
    public static event Action<SignUpRequest> OnSignUpRequest;

    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Button _btnSignUp;
    [SerializeField] private Button _btnBack;
    [SerializeField] private TextMeshProUGUI _message;

    private void OnEnable()
    {
        AuthManager.OnSignUpSuccess += HandleOnSignUpSuccess;
        AuthManager.OnSignUpFailed += HandleOnSignUpFailed;
    }

    private void OnDisable()
    {
        AuthManager.OnSignUpSuccess -= HandleOnSignUpSuccess;
        AuthManager.OnSignUpFailed -= HandleOnSignUpFailed;

        ResetForm();
    }

    void Start()
    {
        _btnSignUp.onClick.AddListener(HandleSignUp);
        _btnBack.onClick.AddListener(() => StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.AUTH_MENU)));
    }

    void HandleSignUp()
    {
        _btnSignUp.interactable = false;
        OnSignUpRequest?.Invoke(new SignUpRequest(_name.text, _email.text, _password.text));
    }

    void HandleOnSignUpSuccess(string message)
    {
        _message.text = message;
        _message.color = Color.green;
        _message.gameObject.SetActive(true);
        _btnBack.interactable = false;

        StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.MAIN_MENU, 1f));
    }

    void HandleOnSignUpFailed(string message)
    {
        _message.text = message;
        _message.color = Color.red;
        _message.gameObject.SetActive(true);
        _btnSignUp.interactable = true;
    }

    void ResetForm()
    {
        _name.text = "";
        _email.text = "";
        _password.text = "";
        _btnSignUp.interactable = true;
        _btnBack.interactable = true;
        _message.gameObject.SetActive(false);
    }
}