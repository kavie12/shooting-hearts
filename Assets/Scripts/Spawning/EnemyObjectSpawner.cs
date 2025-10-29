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
        InvokeRepeating("SpawnHeart", _heartSpawnInterval, _heartSpawnInterval);
        InvokeRepeating("SpawnCarrot", 0, _carrotSpawnInterval);
    }

    void SpawnHeart()
    {
        GameObject obj = EnemyObjectPool.Instance.GetPooledHeart();

        if (obj != null)
        {
            obj.transform.position = new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z);
            obj.SetActive(true);
        }
    }

    void SpawnCarrot()
    {
        GameObject obj = EnemyObjectPool.Instance.GetPooledCarrot();

        if (obj != null)
        {
            obj.transform.position = new Vector3(_spawnRangeX.RandomValue(), _spawnRangeY.RandomValue(), transform.position.z);
            obj.SetActive(true);
        }
    }
}