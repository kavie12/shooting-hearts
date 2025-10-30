using UnityEngine;

public class EnemyObjectSpawner : MonoBehaviour
{
    [Header("Spawn Ranges")]
    [SerializeField] private FloatRange _spawnRangeX = new FloatRange(-8.5f, 8.5f);
    [SerializeField] private FloatRange _spawnRangeY = new FloatRange(6f, 10f);

    [Header("Spawn Intervals")]
    [SerializeField] private float _heartSpawnInterval = 5f;
    [SerializeField] private float _carrotSpawnInterval = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    void SpawnHeart()
    {
        GameObject obj = EnemyObjectPool.instance.GetPooledHeart();

        if (obj != null)
        {
            obj.transform.position = new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z);
            obj.SetActive(true);
        }
    }

    void SpawnCarrot()
    {
        GameObject obj = EnemyObjectPool.instance.GetPooledCarrot();

        if (obj != null)
        {
            obj.transform.position = new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z);
            obj.SetActive(true);
        }
    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnHeart", _heartSpawnInterval * 2, _heartSpawnInterval);
        InvokeRepeating("SpawnCarrot", _carrotSpawnInterval, _carrotSpawnInterval);
    }

    void StopSpawning()
    {
        CancelInvoke("SpawnHeart");
        CancelInvoke("SpawnCarrot");
    }
}