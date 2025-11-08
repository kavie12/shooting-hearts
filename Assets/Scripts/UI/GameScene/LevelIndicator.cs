using TMPro;
using UnityEngine;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField] private float _displayTime = 2f;
    
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Hide();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<LevelStartedEvent>(DisplayLevelIndicator);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelStartedEvent>(DisplayLevelIndicator);
    }

    private void DisplayLevelIndicator(LevelStartedEvent e)
    {
        _text.text = e.LevelConfig.LevelName;
        Show();
        Invoke(nameof(Hide), _displayTime);
    }

    private void Show()
    {
        _text.enabled = true;
    }

    private void Hide()
    {
        _text.enabled = false;
    }
}
