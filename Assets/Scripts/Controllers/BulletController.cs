using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _deadZone = 6;

    private IBulletFactory _bulletFactory;

    private void Awake()
    {
        _bulletFactory = FindFirstObjectByType<PooledBulletFactory>();
    }

    private void FixedUpdate()
    {
        // Fire the bullet upwards
        _rb.MovePosition(_rb.position + _bulletSpeed * Time.fixedDeltaTime * Vector2.up);

        // Make inactive when reaching deadzone
        if (transform.position.y > _deadZone)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IEnemy>(out var enemy))
        {
            EventBus.Publish(new EnemyDestroyedEvent(enemy.Points));
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        _bulletFactory.ReleaseBullet(gameObject);
    }
}
