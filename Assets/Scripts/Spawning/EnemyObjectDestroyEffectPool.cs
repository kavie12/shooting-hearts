using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectDestroyEffectPool : MonoBehaviour
{
    // Singleton object
    public static EnemyObjectDestroyEffectPool instance;

    [Header("Pool Configs")]
    [SerializeField] private EnemyObjectDestroyEffectPoolConfig[] _poolConfigs;

    private Dictionary<EnemyObject, List<GameObject>> _pools;

    private void Awake()
    {
        // Singleton init
        if (instance == null)
        {
            instance = this;

            _pools = new Dictionary<EnemyObject, List<GameObject>>();

            // Init pools
            foreach (EnemyObjectDestroyEffectPoolConfig poolConfig in _poolConfigs)
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

    public GameObject GetPooledEffect(EnemyObject enemyObject)
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
