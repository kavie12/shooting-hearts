using UnityEngine;

public abstract class BaseMenuState : MonoBehaviour
{
    protected MenuSceneManager _menuStateManager;

    private void Awake()
    {
        _menuStateManager = GameObject.FindGameObjectWithTag("MenuSceneManager").GetComponent<MenuSceneManager>();
    }

    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
}