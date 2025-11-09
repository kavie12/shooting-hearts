using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private IBulletFactory _bulletFactory;

    private void Awake()
    {
        _bulletFactory = FindFirstObjectByType<PooledBulletFactory>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerShootEvent>(HandlePlayerShoot);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerShootEvent>(HandlePlayerShoot);
    }

    private void HandlePlayerShoot(PlayerShootEvent e)
    {
        GameObject bullet = _bulletFactory.CreateBullet();
        
        if (bullet != null)
        {
            bullet.transform.position = e.Position;
        }
    }
}