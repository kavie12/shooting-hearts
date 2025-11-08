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

    private void Start()
    {
        _btnLogin.onClick.AddListener(HandleLoginButtonClick);
        _btnBack.onClick.AddListener(() => EventBus.Publish(new LoginFormBackButtonClickEvent()));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnLoginSuccessEvent>(HandleLoginSuccess);
        EventBus.Subscribe<OnLoginFailedEvent>(HandleLoginFailed);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnLoginSuccessEvent>(HandleLoginSuccess);
        EventBus.Unsubscribe<OnLoginFailedEvent>(HandleLoginFailed);
        
        ResetForm();
    }

    private void HandleLoginButtonClick()
    {
        _btnLogin.interactable = false;
        EventBus.Publish(new LoginFormLoginButtonClickEvent(_email.text, _password.text));
    }

    private void HandleLoginSuccess(OnLoginSuccessEvent eventData)
    {
        _message.text = eventData.Message;
        _message.color = Color.green;
        _message.gameObject.SetActive(true);
    }

    private void HandleLoginFailed(OnLoginFailedEvent eventData)
    {
        _message.text = eventData.Message;
        _message.color = Color.red;
        _message.gameObject.SetActive(true);
        _btnLogin.interactable = true;
    }

    private void ResetForm()
    {
        _email.text = string.Empty;
        _password.text = string.Empty;
        _btnLogin.interactable = true;
        _message.gameObject.SetActive(false);
    }
}