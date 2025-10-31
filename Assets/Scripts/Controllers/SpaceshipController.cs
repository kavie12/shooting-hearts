using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    public static event Action OnDestroyed;
    public static event Action<int> OnHealthUpdated;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputActionReference _playerMovement;
    [SerializeField] private InputActionReference _playerFire;
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private GameObject _destroyEffectPrefab;

    private Vector2 _moveDirection;
    private Animator _animator;
    private bool _isPowering = false;
    private int _health = 500;

    void Awake()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        OnHealthUpdated?.Invoke(_health);
    }

    void OnEnable()
    {
        _playerFire.action.started += Fire;
        EnemyObjectController.OnCrashed += TakeDamage;
    }

    void OnDisable()
    {
        _playerFire.action.started -= Fire;
        EnemyObjectController.OnCrashed -= TakeDamage;
    }

    void Update()
    {
        _moveDirection = _playerMovement.action.ReadValue<Vector2>();

        // Apply Powering state when moving
        bool shouldPower = _moveDirection != Vector2.zero;
        if (shouldPower != _isPowering)
        {
            _animator.SetBool("isPowering", shouldPower);
            _isPowering = shouldPower;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        GameObject bullet = BulletPool.instance.GetPooledBullet();

        if (bullet != null)
        {
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
            bullet.SetActive(true);
        }
    }

    private void TakeDamage(int amount)
    {
        _health -= amount;
        OnHealthUpdated?.Invoke(_health);

        if (_health <= 0)
        {
            DestroySpaceship();
        }
    }

    private void DestroySpaceship()
    {
        // Play destroy effect
        Instantiate(_destroyEffectPrefab, transform.position, transform.rotation);
        
        // Destroy the instance
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
