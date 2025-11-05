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
        SpaceshipController.OnHealthUpdated += UpdateHealth;
    }

    private void OnDisable()
    {
        SpaceshipController.OnHealthUpdated -= UpdateHealth;
    }

    void UpdateHealth(int health)
    {
        _slider.value = health;
    }
}
