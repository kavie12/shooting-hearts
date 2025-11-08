using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private void OnEnable()
    {
        EventBus.Subscribe<GameStartEvent>(SpawnPlayer);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartEvent>(SpawnPlayer);
    }

    private void SpawnPlayer(GameStartEvent e)
    {
        Instantiate(_prefab);
    }
}