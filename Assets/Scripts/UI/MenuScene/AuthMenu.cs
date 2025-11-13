using UnityEngine;
using UnityEngine.UI;

public class AuthMenu : MonoBehaviour
{
    [SerializeField] private Button _btnLogin;
    [SerializeField] private Button _btnSignUp;
    [SerializeField] private Button _btnExit;

    private void Start()
    {
        _btnLogin.onClick.AddListener(() => EventBus.Publish(new AuthMenuLoginButtonClickEvent()));
        _btnSignUp.onClick.AddListener(() => EventBus.Publish(new AuthMenuSignUpButtonClickEvent()));
        _btnExit.onClick.AddListener(() => EventBus.Publish(new MenuSceneExitButtonClickEvent()));

        _btnLogin.interactable = false;
        _btnSignUp.interactable = false;
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnAuthMenuVerifyTokenFailedEvent>(HandleStartUpAuthFailed);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnAuthMenuVerifyTokenFailedEvent>(HandleStartUpAuthFailed);
    }

    private void HandleStartUpAuthFailed(OnAuthMenuVerifyTokenFailedEvent e)
    {
        _btnLogin.interactable = true;
        _btnSignUp.interactable = true;
    }
}