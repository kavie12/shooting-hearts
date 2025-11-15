using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _btnResume;
    [SerializeField] private Button _btnMainMenu;

    private void Start()
    {
        _btnResume.onClick.AddListener(() => { });
        _btnMainMenu.onClick.AddListener(() => { });
    }
}