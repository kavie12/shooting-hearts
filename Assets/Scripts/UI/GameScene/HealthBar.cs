using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerHealthUpdatedEvent>(HandlePlayerHealthUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerHealthUpdatedEvent>(HandlePlayerHealthUpdate);
    }

    void HandlePlayerHealthUpdate(PlayerHealthUpdatedEvent e)
    {
        _slider.value = e.NewHealth;
    }
}
