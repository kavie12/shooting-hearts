using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float fallingSpeed = 30f;
    [SerializeField] private float deadZone = -8;

    void FixedUpdate()
    {
        // Make the object fall
        rb.MovePosition(rb.position + Vector2.down * fallingSpeed * Time.fixedDeltaTime);

        // Make inactive when reaching deadzone
        if (transform.position.y < deadZone)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
