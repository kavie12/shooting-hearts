using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _deadZone = 6;

    private void FixedUpdate()
    {
        // Fire the bullet upwards
        _rb.MovePosition(_rb.position + Vector2.up * _bulletSpeed * Time.fixedDeltaTime);

        // Make inactive when reaching deadzone
        if (transform.position.y > _deadZone)
        {
            gameObject.SetActive(false);
        }
    }
}
