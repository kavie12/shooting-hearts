using System;
using UnityEngine;

public class EnemyObjectController : MonoBehaviour
{
    public static event Action<int> OnDestroyed;
    public static event Action<int> OnCrashed;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _fallingSpeed = 6f;
    [SerializeField] private float _deadZone = -8;
    [SerializeField] private int _points = 100;

    void FixedUpdate()
    {
        // Make the object fall
        _rb.MovePosition(_rb.position + Vector2.down * _fallingSpeed * Time.fixedDeltaTime);

        // Make inactive when reaching deadzone
        if (transform.position.y < _deadZone)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            OnDestroyed?.Invoke(_points);
        }

        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            OnCrashed?.Invoke(_points);
        }
    }
}
