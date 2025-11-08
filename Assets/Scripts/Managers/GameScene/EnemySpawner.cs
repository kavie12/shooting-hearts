using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Ranges")]
    [SerializeField] private FloatRange _spawnRangeX = new(-8.5f, 8.5f);
    [SerializeField] private FloatRange _spawnRangeY = new(6f, 10f);

    private EnemyConfig[] _spawnConfigs;
    private IEnemyFactory _enemyFactory;

    private void Awake()
    {
        _enemyFactory = FindFirstObjectByType<PooledEnemyFactory>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<LevelLoadedEvent>(StartSpawning);
        EventBus.Subscribe<LevelCompletedEvent>(HandleLevelCompleted);
        EventBus.Subscribe<GameStopEvent>(HandleGameStop);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelLoadedEvent>(StartSpawning);
        EventBus.Unsubscribe<LevelCompletedEvent>(HandleLevelCompleted);
        EventBus.Unsubscribe<GameStopEvent>(HandleGameStop);
    }

    private void HandleLevelCompleted(LevelCompletedEvent e)
    {
        StopAllCoroutines();
    }

    private void HandleGameStop(GameStopEvent e)
    {
        StopAllCoroutines();
    }

    void StartSpawning(LevelLoadedEvent e)
    {
        _spawnConfigs = e.LevelConfig.EnemyConfigs;

        for (int i = 0; i < _spawnConfigs.Length; i++)
        {
            StartCoroutine(SpawnEnemies(_spawnConfigs[i]));
        }
    }

    IEnumerator SpawnEnemies(EnemyConfig config)
    {
        while (true)
        {
            GameObject enemy = _enemyFactory.CreateEnemy(config.Type);

            if (enemy != null)
            {
                enemy.transform.position = new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z);
            }

            yield return new WaitForSeconds(config.SpawnOverTime);
        }
    }
}