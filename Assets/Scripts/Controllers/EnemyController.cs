using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private float _fallSpeed = 5f;
    [SerializeField] private float _deadZone = -8;
    [SerializeField] private int _points = 100;
    [SerializeField] private int _damage = 100;

    private Rigidbody2D _rb;
    private IEnemyFactory _enemyFactory;

    public EnemyType EnemyType => _enemyType;
    public int Points => _points;
    public int Damage => _damage;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyFactory = FindFirstObjectByType<PooledEnemyFactory>();
    }

    void FixedUpdate()
    {
        // Make the object fall
        _rb.MovePosition(_rb.position + _fallSpeed * Time.fixedDeltaTime * Vector2.down);

        // Make inactive when reaching deadzone
        if (transform.position.y < _deadZone)
        {
            DestroyEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            EventBus.Publish(new EnemyDestroyedEvent(_enemyType, transform.position, _points));
            DestroyEnemy();
        }

        if (collision.CompareTag("Player"))
        {
            DestroyEnemy();
        }
    }

    public void Initialize(EnemyConfig enemyConfig)
    {
        _fallSpeed = enemyConfig.FallSpeed;
    }

    void DestroyEnemy()
    {
        _enemyFactory.ReleaseEnemy(gameObject);
    }
}
