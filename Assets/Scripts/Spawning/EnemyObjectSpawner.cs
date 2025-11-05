using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class EnemyObjectSpawner : MonoBehaviour
{
    [Header("Spawn Ranges")]
    [SerializeField] private FloatRange _spawnRangeX = new(-8.5f, 8.5f);
    [SerializeField] private FloatRange _spawnRangeY = new(6f, 10f);

    [Header("Spawn Config")]
    [SerializeField] private float _spawnDelay = 4f;

    private EnemyObjectSpawnConfig[] _spawnConfigs;

    private void OnEnable()
    {
        LevelManager.OnLevelStarted += StartSpawning;
        LevelManager.OnLevelEnded += StopSpawning;
        GameManager.StopLevelProgression += StopSpawning;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelStarted -= StartSpawning;
        LevelManager.OnLevelEnded -= StopSpawning;
        GameManager.StopLevelProgression -= StopSpawning;
    }

    void StartSpawning(LevelConfig levelConfig)
    {
        // Update configs
        _spawnConfigs = levelConfig.enemyObjectSpawnConfig;

        for (int i = 0; i < _spawnConfigs.Length; i++)
        {
            StartCoroutine(SpawnObjects(_spawnConfigs[i]));
        }
    }

    IEnumerator SpawnObjects(EnemyObjectSpawnConfig config)
    {
        yield return new WaitForSeconds(_spawnDelay);
        while (true)
        {
            GameObject obj = Instantiate(config.Prefab, new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z), transform.rotation);
            yield return new WaitForSeconds(config.SpawnOverTime);
        }
    }

    void StopSpawning()
    {
        StopAllCoroutines();
    }
}