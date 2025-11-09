using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
