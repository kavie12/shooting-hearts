using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manages the reset password form UI in the menu scene
public class ResetPasswordForm : MonoBehaviour
{
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private Button _btnReset;
    [SerializeField] private Button _btnBack;
    [SerializeField] private TextMeshProUGUI _message;

    private void Start()
    {
        _btnReset.onClick.AddListener(HandleResetButtonClick);
        _btnBack.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.ResetPasswordFormBackButton)));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnResetPasswordEmailRequestCompleted>(HandleResetPasswordEmailRequestComplete);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnResetPasswordEmailRequestCompleted>(HandleResetPasswordEmailRequestComplete);

        ResetForm();
    }

    private void HandleResetButtonClick()
    {
        _btnReset.interactable = false;
        EventBus.Publish(new OnResetPasswordEmailRequested(_email.text));
    }

    private void HandleResetPasswordEmailRequestComplete(OnResetPasswordEmailRequestCompleted e)
    {
        _message.text = e.Message;

        if (e.Success)
        {
            _message.color = Color.green;
        }
        else
        {
            _message.color = Color.red;
            _btnReset.interactable = true;
        }

        _message.gameObject.SetActive(true);
    }

    private void ResetForm()
    {
        _email.text = string.Empty;
        _btnReset.interactable = true;
        _message.gameObject.SetActive(false);
    }
}