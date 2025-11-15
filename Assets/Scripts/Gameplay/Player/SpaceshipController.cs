using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _shootOverTime = 0.2f;

    [Header("Effects")]
    [SerializeField] private GameObject _damageFx;
    [SerializeField] private GameObject _destroyFx;
    [SerializeField] private AudioSource _destroySfx;

    private Rigidbody2D _rb;
    private IBulletFactory _bulletFactory;

    private bool _canFire = true;
    private bool _isFiring = false;
    private float _nextShootTime;

    private bool _canMove = true;
    private int _horizontalMoveValue;
    private int _verticalMoveValue;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bulletFactory = FindAnyObjectByType<PooledBulletFactory>();
    }

    void Update()
    {
        HandleMovementInputs();
        HandleShootingInputs();

        if (_canFire && _isFiring && Time.time >= _nextShootTime)
        {
            _nextShootTime = Time.time + _shootOverTime;
            FireBullet();
        }
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * new Vector2(_horizontalMoveValue, _verticalMoveValue));
        }
    }

    void OnEnable()
    {
        EventBus.Subscribe<OnPlayerHealthOver>(HandleHealthOver);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerHealthOver>(HandleHealthOver);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IEnemy>(out var enemy))
        {
            Instantiate(_damageFx, transform.position, transform.rotation);
            EventBus.Publish(new OnPlayerDamaged(enemy.Damage));
        }
    }

    private void HandleMovementInputs()
    {
        _horizontalMoveValue = 0;
        _verticalMoveValue = 0;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            _verticalMoveValue = 1;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            _verticalMoveValue = -1;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            _horizontalMoveValue = 1;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            _horizontalMoveValue = -1;
    }

    private void HandleShootingInputs()
    {
        _isFiring = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0);
    }

    private void FireBullet()
    {
        var bullet = _bulletFactory.CreateBullet();
        bullet.transform.position = transform.position;
    }

    private void HandleHealthOver(OnPlayerHealthOver e)
    {
        Instantiate(_destroyFx, transform.position, transform.rotation);
        _destroySfx.Play();
        EventBus.Publish(new OnPlayerDestroyed());
        Destroy(gameObject);
    }
}