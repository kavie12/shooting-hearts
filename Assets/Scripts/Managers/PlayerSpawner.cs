using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private void OnEnable()
    {
        EventBus.Subscribe<GameStartEvent>(HandleGameStart);
        EventBus.Subscribe<GameContinueEvent>(HandleGameContinue);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartEvent>(HandleGameStart);
        EventBus.Unsubscribe<GameContinueEvent>(HandleGameContinue);
    }

    private void HandleGameStart(GameStartEvent e)
    {
        SpawnPlayer();
    }

    private void HandleGameContinue(GameContinueEvent e)
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(_prefab);
    }
}