using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // Singleton object
    public static BulletPool instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 5;

    private List<GameObject> bulletPool = new List<GameObject>();

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
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, transform);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }

    public GameObject getPooledBullet()
    {
        for (int i = 0;i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return null;
    }
}
