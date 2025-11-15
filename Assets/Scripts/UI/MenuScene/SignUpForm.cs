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
        _btnBack.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.SignUpFormBackButton)));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnSignUpRequestComplete>(HandleSignUpRequestComplete);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnSignUpRequestComplete>(HandleSignUpRequestComplete);

        ResetForm();
    }

    private void HandleSignUpButtonClick()
    {
        _btnSignUp.interactable = false;
        EventBus.Publish(new OnSignUpRequest(_name.text, _email.text, _password.text));
    }

    private void HandleSignUpRequestComplete(OnSignUpRequestComplete e)
    {
        _message.text = e.Message;

        if (e.Success)
        {
            _message.color = Color.green;
        }
        else
        {
            _message.color = Color.red;
            _btnSignUp.interactable = true;
        }

        _message.gameObject.SetActive(true);
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