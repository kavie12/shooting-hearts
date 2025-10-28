using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputActionReference playerMovement;
    [SerializeField] private InputActionReference playerFire;
    [SerializeField] private float moveSpeed = 20f;

    private Vector2 moveDirection;
    private Animator animator;
    private bool isPowering = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerMovement.action.ReadValue<Vector2>();

        // Apply Powering state when moving
        bool shouldPower = moveDirection != Vector2.zero;
        if (shouldPower != isPowering)
        {
            animator.SetBool("isPowering", shouldPower);
            isPowering = shouldPower;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void fireBullet()
    {
        GameObject bullet = BulletPool.instance.getPooledBullet();
        
        if (bullet != null)
        {
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
            bullet.SetActive(true);
        }
    }

    private void OnEnable()
    {
        playerFire.action.started += Fire;
    }
    private void OnDisable()
    {
        playerFire.action.started -= Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        fireBullet();
    }
}
