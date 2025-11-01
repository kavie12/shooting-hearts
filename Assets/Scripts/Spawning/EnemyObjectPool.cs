using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    // Singleton object
    public static EnemyObjectPool instance;

    [Header("Pool Configs")]
    [SerializeField] private EnemyObjectPoolConfig[] _poolConfigs;

    private Dictionary<EnemyObject, List<GameObject>> _pools;

    private void Awake()
    {
        // Singleton init
        if (instance == null)
        {
            instance = this;

            _pools = new Dictionary<EnemyObject, List<GameObject>>();

            // Init pools
            foreach (EnemyObjectPoolConfig poolConfig in _poolConfigs)
            {
                List<GameObject> pool = new List<GameObject>();
                for (int i = 0; i < poolConfig.poolSize; i++)
                {
                    GameObject obj = Instantiate(poolConfig.prefab, transform);
                    obj.SetActive(false);
                    pool.Add(obj);
                }
                _pools.Add(poolConfig.enemyObject, pool);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPooledObject(EnemyObject enemyObject)
    {
        foreach (GameObject obj in _pools[enemyObject])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}