using UnityEngine;
using UnityEngine.UI;

// Auth menu UI component
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
}