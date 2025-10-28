using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Singleton object
    public static ObjectPool instance;

    // List of hearts and carrots objects in the pool
    private List<GameObject> heartPool = new List<GameObject>();
    private List<GameObject> carrotPool = new List<GameObject>();

    // Prefabs
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject carrotPrefab;

    // Pool sizes
    [SerializeField] private int heartPoolSize = 4;
    [SerializeField] private int carrotPoolSize = 10;

    private void Awake()
    {
        // Singleton init
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add items to the pools
        for (int i = 0; i < heartPoolSize; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.SetActive(false);
            heartPool.Add(heart);
        }

        for (int i = 0; i < carrotPoolSize; i++)
        {
            GameObject carrot = Instantiate(carrotPrefab, transform);
            carrot.SetActive(false);
            carrotPool.Add(carrot);
        }
    }

    public GameObject getPooledObject(ObjectType objectType)
    {
        if (objectType == ObjectType.HEART)
        {
            for (int i = 0; i < heartPool.Count; i++)
            {
                if (!heartPool[i].activeInHierarchy)
                {
                    return heartPool[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < carrotPool.Count; i++)
            {
                if (!carrotPool[i].activeInHierarchy)
                {
                    return carrotPool[i];
                }
            }
        }

        return null;
    }
}
