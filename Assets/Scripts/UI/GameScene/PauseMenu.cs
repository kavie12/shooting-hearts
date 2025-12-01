using UnityEngine;
using UnityEngine.UI;

// Buttons in the pause menu
public enum PauseMenuButton
{
    ResumeButton,
    MainMenuButton
}

// Pause menu UI component handling button clicks
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