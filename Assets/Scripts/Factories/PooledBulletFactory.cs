using System.Collections.Generic;
using UnityEngine;

public class PooledBulletFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize = 20;

    private Queue<GameObject> _pool;

    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_prefab, transform);
            bullet.SetActive(false);
            _pool.Enqueue(bullet);
        }
    }

    public GameObject CreateBullet()
    {
        GameObject bullet = _pool.Dequeue();
        
        if (bullet == null) return null;

        bullet.SetActive(true);
        return bullet;
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _pool.Enqueue(bullet);
    }
}