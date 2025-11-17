using UnityEngine;
using UnityEngine.UI;

public enum PauseMenuButton
{
    ResumeButton,
    MainMenuButton
}

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _btnResume;
    [SerializeField] private Button _btnMainMenu;

    private void Start()
    {
        _btnResume.onClick.AddListener(() => EventBus.Publish(new OnPauseMenuButtonClicked(PauseMenuButton.ResumeButton)));
        _btnMainMenu.onClick.AddListener(() => EventBus.Publish(new OnPauseMenuButtonClicked(PauseMenuButton.MainMenuButton)));
    }
}