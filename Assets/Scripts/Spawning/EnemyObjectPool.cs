using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    // Singleton object
    public static EnemyObjectPool instance;

    [SerializeField] private EnemyObjectPrefab[] _enemyObjectPrefabs;
    private Dictionary<EnemyObject, ValueTuple<GameObject, GameObject>> _enemyObjects;

    private EnemyObjectConfig[] _enemyObjectConfigs;

    // In ValueTuple Item1 -> object pool, Item2 -> destroy fx pool
    private Dictionary<EnemyObject, ValueTuple<List<GameObject>, List<GameObject>>> _pools;


    private void OnEnable()
    {
        GameManager.OnLevelChanged += UpdatePools;
    }

    private void OnDisable()
    {
        GameManager.OnLevelChanged -= UpdatePools;
    }

    private void Awake()
    {
        // Singleton init
        if (instance == null)
        {
            instance = this;

            // Init prefab dictionary (Concert the serialized field data to dictionary for easy access)
            _enemyObjects = new Dictionary<EnemyObject, ValueTuple<GameObject, GameObject>>();
            for (int i = 0; i < _enemyObjectPrefabs.Length; i++)
            {
                _enemyObjects.Add(_enemyObjectPrefabs[i].enemyObject, new ValueTuple<GameObject, GameObject>(_enemyObjectPrefabs[i].objectPrefab, _enemyObjectPrefabs[i].objectDestroyFXPrefab));
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPooledObject(EnemyObject enemyObject)
    {
        foreach (GameObject obj in _pools[enemyObject].Item1)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    public GameObject GetPooledFX(EnemyObject enemyObject)
    {
        foreach (GameObject fx in _pools[enemyObject].Item2)
        {
            if (!fx.activeInHierarchy)
            {
                return fx;
            }
        }
        return null;
    }

    private void UpdatePools(LevelConfig level)
    {
        _enemyObjectConfigs = level.enemyObjectConfigs;

        if (_pools == null)
        {
            InitPools();
            return;
        }

        foreach (EnemyObjectConfig config in _enemyObjectConfigs)
        {
            List<GameObject> objectPool = _pools[config.enemyObject].Item1;
            int difference = config.enemyObjectPoolSize - objectPool.Count;
            if (difference > 0)
            {
                for (int i = 0; i < difference; i++)
                {
                    GameObject obj = Instantiate(_enemyObjects[config.enemyObject].Item1, transform);
                    obj.SetActive(false);
                    objectPool.Add(obj);
                }
            }

            List<GameObject> fxPool = _pools[config.enemyObject].Item2;
            difference = config.enemyObjectDestroyFXPoolSize - fxPool.Count;
            if (difference > 0)
            {
                for (int i = 0; i < difference; i++)
                {
                    GameObject fx = Instantiate(_enemyObjects[config.enemyObject].Item2, transform);
                    fx.SetActive(false);
                    fxPool.Add(fx);
                }
            }
        }
    }

    private void InitPools()
    {
        _pools = new Dictionary<EnemyObject, ValueTuple<List<GameObject>, List<GameObject>>>();

        // Init pools
        foreach (EnemyObjectConfig config in _enemyObjectConfigs)
        {
            // Init object pool
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < config.enemyObjectPoolSize; i++)
            {
                GameObject obj = Instantiate(_enemyObjects[config.enemyObject].Item1, transform);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            // Init fx pool
            List<GameObject> fxPool = new List<GameObject>();
            for (int i = 0; i < config.enemyObjectDestroyFXPoolSize; i++)
            {
                GameObject obj = Instantiate(_enemyObjects[config.enemyObject].Item2, transform);
                obj.SetActive(false);
                fxPool.Add(obj);
            }

            _pools.Add(config.enemyObject, new ValueTuple<List<GameObject>, List<GameObject>>(objectPool, fxPool));
        }
    }
}