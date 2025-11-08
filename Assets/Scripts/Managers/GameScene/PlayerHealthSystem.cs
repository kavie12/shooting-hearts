using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 500;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        EventBus.Subscribe<GameStartEvent>(ResetHealth);
        EventBus.Subscribe<PlayerDamagedEvent>(HandlePlayerDamage);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartEvent>(ResetHealth);
        EventBus.Unsubscribe<PlayerDamagedEvent>(HandlePlayerDamage);
    }

    private void HandlePlayerDamage(PlayerDamagedEvent e)
    {
        _currentHealth = Mathf.Min(0, _currentHealth - e.DamageAmount);
        EventBus.Publish(new PlayerHealthUpdatedEvent(_currentHealth));

        if (_currentHealth == 0)
        {
            EventBus.Publish(new PlayerDiedEvent());
        }
    }

    private void ResetHealth(GameStartEvent e)
    {
        _currentHealth = _maxHealth;
        EventBus.Publish(new PlayerHealthUpdatedEvent(_currentHealth));
    }
}