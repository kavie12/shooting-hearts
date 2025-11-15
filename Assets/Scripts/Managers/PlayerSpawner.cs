using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector2 _spawnPosition;

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
        SpawnSpaceship();
    }

    private void HandleLevelRestarted(OnLevelRestarted e)
    {
        SpawnSpaceship();
    }

    private void SpawnSpaceship()
    {
        Instantiate(_playerPrefab, new Vector3(_spawnPosition.x, _spawnPosition.y, 0), transform.rotation);
    }
}