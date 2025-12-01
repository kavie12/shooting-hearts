using UnityEngine;

// Manage the player's health, including damage handling and health updates.
public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 500;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        EventBus.Publish(new OnPlayerHealthUpdated(_currentHealth));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnPlayerDamaged>(HandlePlayerDamage);
        EventBus.Subscribe<OnLevelRestarted>(HandleLevelRestarted);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerDamaged>(HandlePlayerDamage);
        EventBus.Unsubscribe<OnLevelRestarted>(HandleLevelRestarted);
    }

    private void HandlePlayerDamage(OnPlayerDamaged e)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - e.DamageAmount);
        EventBus.Publish(new OnPlayerHealthUpdated(_currentHealth));

        if (_currentHealth == 0)
        {
            EventBus.Publish(new OnPlayerHealthOver());
        }
    }

    private void HandleLevelRestarted(OnLevelRestarted e)
    {
        _currentHealth = _maxHealth;
        EventBus.Publish(new OnPlayerHealthUpdated(_currentHealth));
    }
}