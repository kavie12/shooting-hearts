using UnityEngine;
using UnityEngine.UI;

public class AuthMenu : MonoBehaviour
{
    [SerializeField] private Button _btnLogin;
    [SerializeField] private Button _btnSignUp;
    [SerializeField] private Button _btnExit;

    private void Start()
    {
        _btnLogin.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.AuthMenuLoginButton)));
        _btnSignUp.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.AuthMenuSignUpButton)));
        _btnExit.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.AuthMenuExitButton)));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnTokenAuthenticationRequest>(HandleTokenAuthenticationRequest);
        EventBus.Subscribe<OnTokenAuthenticationRequestComplete>(HandleTokenAuthenticationRequestComplete);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnTokenAuthenticationRequest>(HandleTokenAuthenticationRequest);
        EventBus.Unsubscribe<OnTokenAuthenticationRequestComplete>(HandleTokenAuthenticationRequestComplete);
    }

    private void HandleTokenAuthenticationRequest(OnTokenAuthenticationRequest e)
    {
        _btnLogin.interactable = false;
        _btnSignUp.interactable = false;
    }

    private void HandleTokenAuthenticationRequestComplete(OnTokenAuthenticationRequestComplete e)
    {
        _btnLogin.interactable = true;
        _btnSignUp.interactable = true;
    }
}