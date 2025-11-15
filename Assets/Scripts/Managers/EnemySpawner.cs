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
        EventBus.Subscribe<OnLevelLoaded>(HandleLevelLoaded);
        EventBus.Subscribe<OnLevelStarted>(HandleLevelStarted);
        EventBus.Subscribe<OnLevelStopped>(HandleLevelStopped);
        EventBus.Subscribe<OnLevelCompleted>(HandleLevelEnded);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnLevelLoaded>(HandleLevelLoaded);
        EventBus.Unsubscribe<OnLevelStarted>(HandleLevelStarted);
        EventBus.Unsubscribe<OnLevelStopped>(HandleLevelStopped);
        EventBus.Unsubscribe<OnLevelCompleted>(HandleLevelEnded);
    }

    private void HandleLevelLoaded(OnLevelLoaded e)
    {
        _enemyConfigs = e.LevelConfig.EnemyConfigs;
        _enemyFactory.InitFactory(_enemyConfigs);
    }

    private void HandleLevelStarted(OnLevelStarted e)
    {
        for (int i = 0; i < _enemyConfigs.Length; i++)
        {
            StartCoroutine(SpawnEnemies(_enemyConfigs[i]));
        }
    }

    private void HandleLevelEnded(OnLevelCompleted e)
    {
        StopAllCoroutines();
    }

    private void HandleLevelStopped(OnLevelStopped e)
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemies(EnemyConfig config)
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