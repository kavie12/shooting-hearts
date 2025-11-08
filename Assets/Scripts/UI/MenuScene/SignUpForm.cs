using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignUpForm : MonoBehaviour
{
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Button _btnSignUp;
    [SerializeField] private Button _btnBack;
    [SerializeField] private TextMeshProUGUI _message;

    private void Start()
    {
        _btnSignUp.onClick.AddListener(HandleSignUpButtonClick);
        _btnBack.onClick.AddListener(() => EventBus.Publish(new SignUpFormBackButtonClickEvent()));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnSignUpSuccessEvent>(HandleSignUpSuccess);
        EventBus.Subscribe<OnSignUpFailedEvent>(HandleSignUpFailed);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnSignUpSuccessEvent>(HandleSignUpSuccess);
        EventBus.Unsubscribe<OnSignUpFailedEvent>(HandleSignUpFailed);

        ResetForm();
    }

    private void HandleSignUpButtonClick()
    {
        _btnSignUp.interactable = false;
        EventBus.Publish(new SignUpFormSignUpButtonClickEvent(_name.text, _email.text, _password.text));
    }

    private void HandleSignUpSuccess(OnSignUpSuccessEvent eventData)
    {
        _message.text = eventData.Message;
        _message.color = Color.green;
        _message.gameObject.SetActive(true);
    }

    private void HandleSignUpFailed(OnSignUpFailedEvent eventData)
    {
        _message.text = eventData.Message;
        _message.color = Color.red;
        _message.gameObject.SetActive(true);
        _btnSignUp.interactable = true;
    }

    private void ResetForm()
    {
        _name.text = string.Empty;
        _email.text = string.Empty;
        _password.text = string.Empty;
        _btnSignUp.interactable = true;
        _message.gameObject.SetActive(false);
    }
}