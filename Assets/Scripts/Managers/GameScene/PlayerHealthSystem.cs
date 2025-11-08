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
        EventBus.Subscribe<GameStartEvent>(HandleGameStart);
        EventBus.Subscribe<GameContinueEvent>(HandleLevelReset);
        EventBus.Subscribe<PlayerDamagedEvent>(HandlePlayerDamage);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStartEvent>(HandleGameStart);
        EventBus.Unsubscribe<GameContinueEvent>(HandleLevelReset);
        EventBus.Unsubscribe<PlayerDamagedEvent>(HandlePlayerDamage);
    }

    private void HandleGameStart(GameStartEvent e)
    {
        ResetHealth();
    }

    private void HandleLevelReset(GameContinueEvent e)
    {
        ResetHealth();
    }

    private void HandlePlayerDamage(PlayerDamagedEvent e)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - e.DamageAmount);
        EventBus.Publish(new PlayerHealthUpdatedEvent(_currentHealth));

        if (_currentHealth == 0)
        {
            EventBus.Publish(new PlayerHealthOverEvent());
        }
    }

    private void ResetHealth()
    {
        _currentHealth = _maxHealth;
        EventBus.Publish(new PlayerHealthUpdatedEvent(_currentHealth));
    }
}