using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Ranges")]
    [SerializeField] private FloatRange _spawnRangeX = new(-8.5f, 8.5f);
    [SerializeField] private FloatRange _spawnRangeY = new(6f, 10f);

    private EnemyConfig[] _enemyConfigs;
    private IEnemyFactory _enemyFactory;

    private void Awake()
    {
        _enemyFactory = FindFirstObjectByType<PooledEnemyFactory>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<LevelLoadedEvent>(HandleLevelLoaded);
        EventBus.Subscribe<LevelStartedEvent>(HandleLevelStarted);
        EventBus.Subscribe<LevelCompletedEvent>(HandleLevelCompleted);
        EventBus.Subscribe<LevelStopEvent>(HandleLevelStop);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelLoadedEvent>(HandleLevelLoaded);
        EventBus.Unsubscribe<LevelStartedEvent>(HandleLevelStarted);
        EventBus.Unsubscribe<LevelCompletedEvent>(HandleLevelCompleted);
        EventBus.Unsubscribe<LevelStopEvent>(HandleLevelStop);
    }

    private void HandleLevelCompleted(LevelCompletedEvent e)
    {
        StopAllCoroutines();
    }

    private void HandleLevelStop(LevelStopEvent e)
    {
        StopAllCoroutines();
    }

    private void HandleLevelLoaded(LevelLoadedEvent e)
    {
        _enemyConfigs = e.LevelConfig.EnemyConfigs;
        _enemyFactory.InitFactory(_enemyConfigs);
    }

    void HandleLevelStarted(LevelStartedEvent e)
    {
        for (int i = 0; i < _enemyConfigs.Length; i++)
        {
            StartCoroutine(SpawnEnemies(_enemyConfigs[i]));
        }
    }

    IEnumerator SpawnEnemies(EnemyConfig config)
    {
        yield return new WaitForSeconds(config.SpawnDelay);

        while (true)
        {
            GameObject enemy = _enemyFactory.CreateEnemy(config.EnemyType);

            if (enemy != null)
            {
                enemy.transform.position = new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z);
            }

            yield return new WaitForSeconds(config.SpawnOverTime);
        }
    }
}