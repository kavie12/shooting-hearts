using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputActionReference _playerMovement;
    [SerializeField] private InputActionReference _playerFire;
    [SerializeField] private float _moveSpeed = 8f;

    private Vector2 _moveDirection;
    private Animator _animator;
    private bool _isPowering = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
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

    private void OnEnable()
    {
        _playerFire.action.started += Fire;
    }
    private void OnDisable()
    {
        _playerFire.action.started -= Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        GameObject bullet = BulletPool.Instance.GetPooledBullet();

        if (bullet != null)
        {
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
            bullet.SetActive(true);
        }
    }
}
