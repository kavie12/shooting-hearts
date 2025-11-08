using System.Collections.Generic;
using UnityEngine;

public class PooledEnemyFactory : MonoBehaviour, IEnemyFactory
{
    private Dictionary<EnemyType, Queue<GameObject>> _pools = new();

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
        GameObject enemy = _pools[type].Dequeue();

        if (enemy == null) return null;

        enemy.SetActive(true);
        return enemy;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        _pools[enemy.GetComponent<EnemyController>().Type].Enqueue(enemy);
    }

    private void HandleLevelLoaded(LevelLoadedEvent e)
    {
        _pools = new Dictionary<EnemyType, Queue<GameObject>>();

        foreach (EnemyConfig config in e.LevelConfig.EnemyConfigs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < config.PoolSize; i++)
            {
                GameObject enemy = Instantiate(config.Prefab, transform);
                enemy.SetActive(false);
                pool.Enqueue(enemy);
            }
            _pools.Add(config.Type, pool);
        }
    }
}
