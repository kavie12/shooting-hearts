using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] public EnemyType Type { get; }
    [SerializeField] private float _fallingSpeed = 6f;
    [SerializeField] private float _deadZone = -8;
    [SerializeField] private int _points = 100;
    [SerializeField] private int _damage = 100;

    private Rigidbody2D _rb;
    private IEnemyFactory _enemyFactory;

    public int Points => _points;
    public int Damage => _damage;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Make the object fall
        _rb.MovePosition(_rb.position + _fallingSpeed * Time.fixedDeltaTime * Vector2.down);

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
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        _enemyFactory.ReleaseEnemy(gameObject);
    }
}
