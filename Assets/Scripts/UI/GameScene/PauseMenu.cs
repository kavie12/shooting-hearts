using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _btnResume;
    [SerializeField] private Button _btnMainMenu;
    [SerializeField] private Button _btnQuitGame;

    private void Start()
    {
        _btnResume.onClick.AddListener(HandleResumeButtonClick);
        _btnMainMenu.onClick.AddListener(() => EventBus.Publish(new PauseMenuMainMenuButtonClickEvent()));
        _btnQuitGame.onClick.AddListener(() => EventBus.Publish(new PauseMenuQuitGameButtonClickEvent()));
    }

    private void HandleResumeButtonClick()
    {
        EventBus.Publish(new PauseMenuResumeButtonClickEvent());
        gameObject.SetActive(false);
    }
}