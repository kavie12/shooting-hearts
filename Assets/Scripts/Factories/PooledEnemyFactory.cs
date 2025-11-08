using System.Collections.Generic;
using UnityEngine;

public class PooledEnemyFactory : MonoBehaviour, IEnemyFactory
{
    private Dictionary<EnemyType, Queue<GameObject>> _pools = new();

    private void OnEnable()
    {
        EventBus.Subscribe<LevelLoadedEvent>(InitPool);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelLoadedEvent>(InitPool);
    }

    public GameObject CreateEnemy(EnemyType enemyType)
    {
        if (_pools[enemyType].TryDequeue(out var enemy))
        {
            enemy.SetActive(true);
            return enemy;
        }
        Debug.LogWarning(enemyType.ToString() + " enemy pool is empty. Please increase the size of the pool");
        return null;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        _pools[enemy.GetComponent<EnemyController>().EnemyType].Enqueue(enemy);
    }

    private void InitPool(LevelLoadedEvent e)
    {
        _pools ??= new Dictionary<EnemyType, Queue<GameObject>>();
        _pools.Clear();

        foreach (EnemyConfig config in e.LevelConfig.EnemyConfigs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < config.PoolSize; i++)
            {
                GameObject enemy = Instantiate(config.Prefab, transform);
                enemy.GetComponent<IEnemy>().Initialize(config);
                enemy.SetActive(false);
                pool.Enqueue(enemy);
            }
            _pools.Add(config.EnemyType, pool);
        }
    }
}
