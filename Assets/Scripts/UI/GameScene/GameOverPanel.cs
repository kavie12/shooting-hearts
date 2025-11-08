using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _gameOverText;
    [SerializeField] public TextMeshProUGUI _score;
    [SerializeField] public TextMeshProUGUI _highScore;

    [SerializeField] private Button _btnPlayAgain;
    [SerializeField] private Button _btnMainMenu;

    private void Start()
    {
        _btnPlayAgain.onClick.AddListener(() => EventBus.Publish(new OnGameOverPanelPlayAgainButtonClickedEvent()));
        _btnMainMenu.onClick.AddListener(() => EventBus.Publish(new OnGameOverPanelMainMenuButtonClickedEvent()));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnFinalScoresReadyEvent>(DisplayScores);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnFinalScoresReadyEvent>(DisplayScores);
    }

    private void DisplayScores(OnFinalScoresReadyEvent e)
    {
        _score.text = e.Score.ToString();
        _highScore.text = e.HighScore.ToString();
    }
}
