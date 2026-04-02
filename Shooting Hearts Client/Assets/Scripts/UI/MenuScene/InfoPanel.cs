using UnityEngine;
using UnityEngine.UI;

// Info panel UI component in the menu scene.
public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Button _btnClose;

    private void Start()
    {
        _btnClose.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.InfoPanelCloseButton)));
    }
}