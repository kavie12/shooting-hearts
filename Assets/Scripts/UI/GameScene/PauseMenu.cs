using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _btnQuitGame;

    private void Start()
    {
        _btnQuitGame.onClick.AddListener(() => EventBus.Publish(new PauseMenuQuitGameButtonClickEvent()));
    }
}