using System;
using UnityEngine;

public class EnemyObjectController : MonoBehaviour
{
    public static event Action<int> OnDestroyed;
    public static event Action<int> OnCrashed;

    [SerializeField] private EnemyObject enemyObject;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _fallingSpeed = 6f;
    [SerializeField] private float _deadZone = -8;
    [SerializeField] private int _points = 100;

    private void OnEnable()
    {
        UpdateFallingSpeed(GameManager.instance.GetCurrentLevelConfig());
    }

    void FixedUpdate()
    {
        // Make the object fall
        _rb.MovePosition(_rb.position + _fallingSpeed * Time.fixedDeltaTime * Vector2.down);

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
            DestroyObject();
            OnDestroyed?.Invoke(_points);
        }

        if (collision.CompareTag("Player"))
        {
            DestroyObject();
            OnCrashed?.Invoke(_points);
        }
    }

    void DestroyObject()
    {
        gameObject.SetActive(false);

        // Initiate destroy effect
        GameObject effect = EnemyObjectPool.instance.GetPooledFX(enemyObject);
        if (effect != null)
        {
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
    }

    void UpdateFallingSpeed(LevelConfig level)
    {
        foreach (EnemyObjectConfig config in level.enemyObjectConfigs)
        {
            if (enemyObject == config.enemyObject)
            {
                _fallingSpeed = config.fallingSpeed;
            }
        }
    }
}
