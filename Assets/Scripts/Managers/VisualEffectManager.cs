using System.Collections.Generic;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerDamageEffectPrefab;
    [SerializeField] private GameObject _playerDestroyEffectPrefab;

    private Dictionary<EnemyType, GameObject> _enemyDestroyEffectPrefabs;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<LevelLoadedEvent>(LoadEnemyDestroyEffectPrefabs);
        EventBus.Subscribe<EnemyDestroyedEvent>(PlayEnemyDestroyEffect);
        EventBus.Subscribe<PlayerDamagedEvent>(PlayPlayerDamageEffect);
        EventBus.Subscribe<PlayerDestroyedEvent>(PlayPlayerDestroyEffect);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelLoadedEvent>(LoadEnemyDestroyEffectPrefabs);
        EventBus.Unsubscribe<EnemyDestroyedEvent>(PlayEnemyDestroyEffect);
        EventBus.Unsubscribe<PlayerDamagedEvent>(PlayPlayerDamageEffect);
        EventBus.Unsubscribe<PlayerDestroyedEvent>(PlayPlayerDestroyEffect);
    }

    private void LoadEnemyDestroyEffectPrefabs(LevelLoadedEvent e)
    {
        _enemyDestroyEffectPrefabs ??= new Dictionary<EnemyType, GameObject>();
        _enemyDestroyEffectPrefabs.Clear();

        foreach (EnemyConfig config in e.LevelConfig.EnemyConfigs)
        {
            _enemyDestroyEffectPrefabs.Add(config.EnemyType, config.DestroyEffectPrefab);
        }
    }

    private void PlayEnemyDestroyEffect(EnemyDestroyedEvent e)
    {
        GameObject fx = Instantiate(_enemyDestroyEffectPrefabs[e.EnemyType], e.Position, transform.rotation);
        Destroy(fx, 2f);
    }

    private void PlayPlayerDamageEffect(PlayerDamagedEvent e)
    {
        GameObject fx = Instantiate(_playerDamageEffectPrefab, e.Position, transform.rotation);
        Destroy(fx, 3f);
    }

    private void PlayPlayerDestroyEffect(PlayerDestroyedEvent e)
    {
        GameObject fx = Instantiate(_playerDestroyEffectPrefab, e.Position, transform.rotation);
        Destroy(fx, 5f);
    }
}