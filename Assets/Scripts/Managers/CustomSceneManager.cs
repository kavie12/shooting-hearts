using UnityEngine.SceneManagement;

public enum Scene
{
    MENU_SCENE,
    GAME_SCENE
}

public static class CustomSceneManager
{
    public static void ChangeScene(Scene scene)
    {
        SceneManager.LoadScene((int) scene);
    }
}