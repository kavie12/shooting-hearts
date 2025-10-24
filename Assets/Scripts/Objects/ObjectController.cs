using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fallingSpeed = 3f;
    public float deadZone = -8;

    void FixedUpdate()
    {
        // Make the object fall
        rb.MovePosition(rb.position + (Vector2.down * fallingSpeed * Time.fixedDeltaTime));

        // Make inactive when reaching deadzone
        if (transform.position.y < deadZone)
        {
            gameObject.SetActive(false);
        }
    }
}
