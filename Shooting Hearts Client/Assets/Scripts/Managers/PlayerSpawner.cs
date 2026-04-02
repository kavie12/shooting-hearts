using UnityEngine;

// Manage player spaceship spawning based on game events, such as game start and level restart.
public class PlayerSpawner : MonoBehaviour
{
    private GameObject _playerPrefab;

    private void OnEnable()
    {
        EventBus.Subscribe<OnGameStarted>(HandleGameStarted);
        EventBus.Subscribe<OnLevelRestarted>(HandleLevelRestarted);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnGameStarted>(HandleGameStarted);
        EventBus.Unsubscribe<OnLevelRestarted>(HandleLevelRestarted);
    }

    private void HandleGameStarted(OnGameStarted e)
    {
        _playerPrefab = e.GameConfig.PlayerConfig.Prefab;
        SpawnSpaceship();
    }

    private void HandleLevelRestarted(OnLevelRestarted e)
    {
        SpawnSpaceship();
    }

    private void SpawnSpaceship()
    {
        Instantiate(_playerPrefab, new Vector3(0f, -2.8f, 0f), transform.rotation);
    }
}