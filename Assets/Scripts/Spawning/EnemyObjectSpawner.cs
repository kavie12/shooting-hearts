using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectSpawner : MonoBehaviour
{
    [Header("Spawn Ranges")]
    [SerializeField] private FloatRange _spawnRangeX = new FloatRange(-8.5f, 8.5f);
    [SerializeField] private FloatRange _spawnRangeY = new FloatRange(6f, 10f);

    private EnemyObjectConfig[] _enemyObjectConfigs;

    private void OnEnable()
    {
        SpaceshipController.OnDestroyed += StopSpawning;
        GameManager.OnLevelChanged += HandleLevelChange;
        GameManager.OnGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        SpaceshipController.OnDestroyed -= StopSpawning;
        GameManager.OnLevelChanged -= HandleLevelChange;
        GameManager.OnGameOver -= StopSpawning;
    }

    IEnumerator SpawnObjects(EnemyObjectConfig config)
    {
        yield return new WaitForSeconds(config.spawnDelay);
        while (true)
        {
            GameObject obj = EnemyObjectPool.instance.GetPooledObject(config.enemyObject);
            if (obj != null)
            {
                obj.transform.position = new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z);
                obj.SetActive(true);
            }
            yield return new WaitForSeconds(config.spawnOverTime);
        }
    }

    void StartSpawning()
    {
        for (int i = 0; i < _enemyObjectConfigs.Length; i++)
        {
            StartCoroutine(SpawnObjects(_enemyObjectConfigs[i]));
        }
    }

    void StopSpawning()
    {
        StopAllCoroutines();
    }

    void HandleLevelChange(LevelConfig level)
    {
        StopSpawning();
        _enemyObjectConfigs = level.enemyObjectConfigs;
        StartSpawning();
    }
}