using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AuthMenu : BaseMenuState
{
    public static event Action OnExitClicked;

    [SerializeField] private Button _btnLogin;
    [SerializeField] private Button _btnSignUp;
    [SerializeField] private Button _btnExit;

    void Start()
    {
        _btnLogin.onClick.AddListener(() => StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.LOGIN_FORM)));
        _btnSignUp.onClick.AddListener(() => StartCoroutine(_menuStateManager.ChangeState(MenuSceneState.SIGN_UP_FORM)));
        _btnExit.onClick.AddListener(() => OnExitClicked?.Invoke());
    }
}
