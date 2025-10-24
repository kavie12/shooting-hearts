using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fallingSpeed = 3f;
    public float deadZone = -8;

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2.down * fallingSpeed * Time.fixedDeltaTime));

        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
