using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Slider _healthBar;

    void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    void OnEnable()
    {
        SpaceshipController.OnHealthUpdated += UpdateHealth;
    }

    void OnDisable()
    {
        SpaceshipController.OnHealthUpdated -= UpdateHealth;
    }

    void UpdateHealth(int health)
    {
        _healthBar.value = health;
    }
}
