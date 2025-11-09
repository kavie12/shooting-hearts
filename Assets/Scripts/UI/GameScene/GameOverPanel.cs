using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _gameOverText;
    [SerializeField] public GameObject _scoreSection;
    [SerializeField] public GameObject _loadingSpinner;
    [SerializeField] public TextMeshProUGUI _score;
    [SerializeField] public TextMeshProUGUI _highScore;

    [SerializeField] private Button _btnPlayAgain;
    [SerializeField] private Button _btnMainMenu;

    private void Start()
    {
        _btnPlayAgain.onClick.AddListener(() => EventBus.Publish(new GameOverPanelPlayAgainButtonClickedEvent()));
        _btnMainMenu.onClick.AddListener(() => EventBus.Publish(new GameOverPanelMainMenuButtonClickedEvent()));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnFinalScoresReadyEvent>(DisplayScores);

        _scoreSection.SetActive(false);
        _loadingSpinner.SetActive(true);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnFinalScoresReadyEvent>(DisplayScores);
    }

    private void DisplayScores(OnFinalScoresReadyEvent e)
    {
        _score.text = e.Score.ToString();
        _highScore.text = e.HighScore.ToString();

        _scoreSection.SetActive(true);
        _loadingSpinner.SetActive(false);
    }
}
