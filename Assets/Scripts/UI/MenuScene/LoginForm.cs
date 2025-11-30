using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginForm : MonoBehaviour
{
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Button _btnLogin;
    [SerializeField] private Button _btnBack;
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private Button _btnResetPassword;

    private void Start()
    {
        _btnLogin.onClick.AddListener(HandleLoginButtonClick);
        _btnBack.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.LoginFormBackButton)));
        _btnResetPassword.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.LoginFormResetPasswordButton)));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnLoginRequestComplete>(HandleLoginRequestComplete);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnLoginRequestComplete>(HandleLoginRequestComplete);
        
        ResetForm();
    }

    private void HandleLoginButtonClick()
    {
        _btnLogin.interactable = false;
        EventBus.Publish(new OnLoginRequest(_email.text, _password.text));
    }

    private void HandleLoginRequestComplete(OnLoginRequestComplete e)
    {
        _message.text = e.Message;

        if (e.Success)
        {
            _message.color = Color.green;
        }
        else
        {
            _message.color = Color.red;
            _btnLogin.interactable = true;
        }

        _message.gameObject.SetActive(true);
    }

    private void ResetForm()
    {
        _email.text = string.Empty;
        _password.text = string.Empty;
        _btnLogin.interactable = true;
        _message.gameObject.SetActive(false);
    }
}