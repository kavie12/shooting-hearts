using TMPro;
using UnityEngine;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField] private float _displayTime = 2f;
    
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayLevelName(string levelName)
    {
        _text.text = levelName;
        Invoke(nameof(Hide), _displayTime);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
