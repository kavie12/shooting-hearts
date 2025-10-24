using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    // List of hearts and carrots objects in the pool
    private List<GameObject> heartPool = new List<GameObject>();
    private List<GameObject> carrotPool = new List<GameObject>();

    // Prefabs
    public GameObject heartPrefab;
    public GameObject carrotPrefab;

    // Pool sizes
    public int heartPoolSize = 4;
    public int carrotPoolSize = 10;

    private void Awake()
    {
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
            GameObject heart = Instantiate(heartPrefab);
            heart.SetActive(false);
            heartPool.Add(heart);
        }

        for (int i = 0; i < carrotPoolSize; i++)
        {
            GameObject carrot = Instantiate(carrotPrefab);
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
