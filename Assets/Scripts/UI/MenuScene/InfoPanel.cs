using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Button _btnClose;

    private void Start()
    {
        _btnClose.onClick.AddListener(() => EventBus.Publish(new OnMenuSceneButtonClick(MenuSceneButton.InfoPanelCloseButton)));
    }
}