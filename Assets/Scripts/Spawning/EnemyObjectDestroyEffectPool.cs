using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectDestroyEffectPool : MonoBehaviour
{
    // Singleton object
    public static EnemyObjectDestroyEffectPool instance;

    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private int _poolSize = 3;

    private List<GameObject> _effectPool = new List<GameObject>();

    private void Awake()
    {
        // Singleton init
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_effectPrefab, transform);
            obj.SetActive(false);
            _effectPool.Add(obj);
        }
    }

    public GameObject GetPooledEffect()
    {
        for (int i = 0; i < _effectPool.Count; i++)
        {
            if (!_effectPool[i].activeInHierarchy)
            {
                return _effectPool[i];
            }
        }

        return null;
    }
}
