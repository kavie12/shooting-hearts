using UnityEngine;
using UnityEngine.UI;

// Displays and updates the player's health bar in the game scene
public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnPlayerHealthUpdated>(HandlePlayerHealthUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerHealthUpdated>(HandlePlayerHealthUpdate);
    }

    void HandlePlayerHealthUpdate(OnPlayerHealthUpdated e)
    {
        _slider.value = e.NewHealth;
    }
}
