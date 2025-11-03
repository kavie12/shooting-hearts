using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    public static event Action OnDestroyed;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputActionReference _playerMovement;
    [SerializeField] private InputActionReference _playerFire;
    [SerializeField] private float _moveSpeed = 8f;

    [Header("Effects")]
    [SerializeField] private GameObject _destroyEffectPrefab;
    [SerializeField] private GameObject _damageEffectPrefab;

    [Header("SFX")]
    [SerializeField] private AudioSource _shootSxf;

    private Slider _healthBar;
    private Vector2 _moveDirection;
    private int _health = 500;

    void Awake()
    {
        _healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();
        _healthBar.value = _health;
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
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * _moveDirection);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        GameObject bullet = BulletPool.instance.GetPooledBullet();

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            _shootSxf.Play();
        }
    }

    private void TakeDamage(int amount)
    {
        _health -= amount;
        _healthBar.value = _health;

        if (_health <= 0)
        {
            DestroySpaceship();
        }
        else
        {
            // Play damage effect
            GameObject fx = Instantiate(_damageEffectPrefab, transform.position, transform.rotation);
            Destroy(fx, 5f);
        }
    }

    private void DestroySpaceship()
    {
        // Play destroy FX
        GameObject fx = Instantiate(_destroyEffectPrefab, transform.position, transform.rotation);
        Destroy(fx, 5f);

        // Destroy the instance
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
