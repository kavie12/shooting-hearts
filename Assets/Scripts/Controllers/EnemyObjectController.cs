using System;
using UnityEngine;

public class EnemyObjectController : MonoBehaviour
{
    public static event Action<int> OnDestroyed;
    public static event Action<int> OnCrashed;

    [SerializeField] private float _fallingSpeed = 6f;
    [SerializeField] private float _deadZone = -8;
    [SerializeField] private int _points = 100;
    [SerializeField] private GameObject _destroyFX;

    private Rigidbody2D _rb;

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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            OnDestroyed?.Invoke(_points);
            DestroyObject();
        }

        if (collision.CompareTag("Player"))
        {
            OnCrashed?.Invoke(_points);
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        // Initiate destroy effect
        GameObject fx = Instantiate(_destroyFX, transform.position, transform.rotation);
        Destroy(fx, 4f);

        Destroy(gameObject);
    }
}
