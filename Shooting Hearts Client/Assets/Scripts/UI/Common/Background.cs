using UnityEngine;

// Singleton class to manage the background object across different scenes
public class Background : MonoBehaviour
{
    // Singleton for DontDestroyOnLoad (Persistance across the scenes)
    private static Background Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
