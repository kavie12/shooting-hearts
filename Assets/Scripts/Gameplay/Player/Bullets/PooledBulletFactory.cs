using System.Collections.Generic;
using UnityEngine;

public class PooledBulletFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize = 6;

    private Queue<GameObject> _pool;

    private void Start()
    {
        _pool = new Queue<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_prefab, transform);
            bullet.SetActive(false);
            _pool.Enqueue(bullet);
        }
    }

    public GameObject CreateBullet()
    {
        if (_pool.TryDequeue(out var bullet))
        {
            bullet.SetActive(true);
            return bullet;
        }
        Debug.LogWarning("Bullet pool is empty. Please increase the size of the pool");
        return null;
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _pool.Enqueue(bullet);
    }
}