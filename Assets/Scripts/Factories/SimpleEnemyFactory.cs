using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyFactory : MonoBehaviour, IEnemyFactory
{
    private EnemyConfig[] enemyConfigs;
    private Dictionary<EnemyType, GameObject> _prefabs;

    private void OnEnable()
    {
        EventBus.Subscribe<LevelLoadedEvent>(HandleLevelLoaded);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelLoadedEvent>(HandleLevelLoaded);
    }

    public GameObject CreateEnemy(EnemyType type)
    {
        return Instantiate(_prefabs[type], transform);
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }

    private void HandleLevelLoaded(LevelLoadedEvent e)
    {
        enemyConfigs = e.LevelConfig.EnemyConfigs;

        _prefabs.Clear();
        foreach (EnemyConfig config in enemyConfigs) {
            _prefabs.Add(config.Type, config.Prefab);
        }
    }
}