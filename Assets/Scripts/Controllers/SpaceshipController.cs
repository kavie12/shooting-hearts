using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] private InputActionReference _playerMovement;
    [SerializeField] private InputActionReference _playerFire;
    [SerializeField] private float _moveSpeed = 8f;

    private Rigidbody2D _rb;
    private Vector2 _moveDirection;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _playerFire.action.started += Fire;

        EventBus.Subscribe<PlayerDiedEvent>(DestroySpaceship);

    }

    void OnDisable()
    {
        _playerFire.action.started -= Fire;

        EventBus.Unsubscribe<PlayerDiedEvent>(DestroySpaceship);
    }

    void Update()
    {
        _moveDirection = _playerMovement.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * _moveDirection);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        EventBus.Publish(new PlayerShootEvent(transform.position));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IEnemy>(out var damageDealer))
        {
            EventBus.Publish(new PlayerDamagedEvent(damageDealer.Damage));
        }
    }

    private void DestroySpaceship(PlayerDiedEvent e)
    {
        Destroy(gameObject);
    }
}