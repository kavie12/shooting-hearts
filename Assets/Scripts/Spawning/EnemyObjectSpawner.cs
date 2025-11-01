using System;
using System.Collections;
using UnityEngine;

public class EnemyObjectSpawner : MonoBehaviour
{
    [Header("Spawn Ranges")]
    [SerializeField] private FloatRange _spawnRangeX = new FloatRange(-8.5f, 8.5f);
    [SerializeField] private FloatRange _spawnRangeY = new FloatRange(6f, 10f);

    [Header("Spawn Configs")]
    [SerializeField] private EnemyObjectSpawnConfig[] _spawnConfigs;

    private Coroutine[] _spawnCoroutines;

    private void Awake()
    {
        _spawnCoroutines = new Coroutine[_spawnConfigs.Length];
    }

    void Start()
    {
        StartSpawning();
    }

    private void OnEnable()
    {
        SpaceshipController.OnDestroyed += StopSpawning;
        BonusChancePanelController.OnAnswerCorrect += StartSpawning;
    }

    private void OnDisable()
    {
        SpaceshipController.OnDestroyed -= StopSpawning;
        BonusChancePanelController.OnAnswerCorrect -= StartSpawning;
    }

    IEnumerator SpawnObjects(EnemyObjectSpawnConfig spawnConfig)
    {
        yield return new WaitForSeconds(spawnConfig.spawnDelay);
        while (true)
        {
            GameObject obj = EnemyObjectPool.instance.GetPooledObject(spawnConfig.enemyObject);
            if (obj != null)
            {
                obj.transform.position = new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z);
                obj.SetActive(true);
            }
            yield return new WaitForSeconds(spawnConfig.spawnOverTime);
        }
    }

    void StartSpawning()
    {
        for (int i = 0; i < _spawnConfigs.Length; i++)
        {
            _spawnCoroutines[i] = StartCoroutine(SpawnObjects(_spawnConfigs[i]));
        }
    }

    void StopSpawning()
    {
        for (int i = 0; i < _spawnConfigs.Length; i++)
        {
            if (_spawnCoroutines[i] != null)
            {
                StopCoroutine(_spawnCoroutines[i]);
            }
        }
    }
}