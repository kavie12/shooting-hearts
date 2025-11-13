using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private void OnEnable()
    {
        EventBus.Subscribe<GameStartEvent>(HandleGameStart);
        EventBus.Subscribe<LevelRestartEvent>(HandleLevelRestart);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartEvent>(HandleGameStart);
        EventBus.Unsubscribe<LevelRestartEvent>(HandleLevelRestart);
    }

    private void HandleGameStart(GameStartEvent e)
    {
        SpawnPlayer();
    }

    private void HandleLevelRestart(LevelRestartEvent e)
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(_prefab);
    }
}