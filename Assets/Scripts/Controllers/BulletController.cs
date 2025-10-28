using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float deadZone = 6;

    private void FixedUpdate()
    {
        // Fire the bullet upwards
        rb.MovePosition(rb.position + Vector2.up * bulletSpeed * Time.fixedDeltaTime);

        // Make inactive when reaching deadzone
        if (transform.position.y > deadZone)
        {
            gameObject.SetActive(false);
        }
    }
}
