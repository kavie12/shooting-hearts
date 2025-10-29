using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // Singleton object
    public static BulletPool Instance;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _poolSize = 5;

    private List<GameObject> _bulletPool = new List<GameObject>();

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
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_bulletPrefab, transform);
            obj.SetActive(false);
            _bulletPool.Add(obj);
        }
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0;i < _bulletPool.Count; i++)
        {
            if (!_bulletPool[i].activeInHierarchy)
            {
                return _bulletPool[i];
            }
        }

        return null;
    }
}
