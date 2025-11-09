using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        EventBus.Subscribe<GameOverEvent>(DisplayGameOverPanel);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameOverEvent>(DisplayGameOverPanel);
    }

    private void DisplayGameOverPanel(GameOverEvent e)
    {
        _gameOverPanel.SetActive(true);
    }
}