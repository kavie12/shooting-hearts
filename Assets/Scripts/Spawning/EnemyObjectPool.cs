using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    // Singleton object
    public static EnemyObjectPool Instance;

    [Header("Heart Pool")]
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private int _heartPoolSize = 5;
    private List<GameObject> _heartPool = new List<GameObject>();

    [Header("Carrot Pool")]
    [SerializeField] private GameObject _carrotPrefab;
    [SerializeField] private int _carrotPoolSize = 10;
    private List<GameObject> _carrotPool = new List<GameObject>();

    private void Awake()
    {
        // Singleton init
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add items to the pools
        for (int i = 0; i < _heartPoolSize; i++)
        {
            GameObject heart = Instantiate(_heartPrefab, transform);
            heart.SetActive(false);
            _heartPool.Add(heart);
        }

        for (int i = 0; i < _carrotPoolSize; i++)
        {
            GameObject carrot = Instantiate(_carrotPrefab, transform);
            carrot.SetActive(false);
            _carrotPool.Add(carrot);
        }
    }

    public GameObject GetPooledHeart()
    {
        for (int i = 0; i < _heartPool.Count; i++)
        {
            if (!_heartPool[i].activeInHierarchy)
            {
                return _heartPool[i];
            }
        }
        return null;
    }

    public GameObject GetPooledCarrot()
    {
        for (int i = 0; i < _carrotPool.Count; i++)
        {
            if (!_carrotPool[i].activeInHierarchy)
            {
                return _carrotPool[i];
            }
        }
        return null;
    }
}