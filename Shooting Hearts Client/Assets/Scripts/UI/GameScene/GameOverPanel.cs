using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Buttons in the game over panel
public enum GameOverPanelButton
{
    PlayAgainButton,
    MainMenuButton
}

// Manage the game over panel UI, including displaying scores and handling button interactions.
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
        _btnPlayAgain.onClick.AddListener(() => EventBus.Publish(new OnGameOverPanelButtonClicked(GameOverPanelButton.PlayAgainButton)));
        _btnMainMenu.onClick.AddListener(() => EventBus.Publish(new OnGameOverPanelButtonClicked(GameOverPanelButton.MainMenuButton)));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnUpdatedHighScoreFetched>(HandleUpdatedHighScoreFetched);

        _scoreSection.SetActive(false);
        _loadingSpinner.SetActive(true);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnUpdatedHighScoreFetched>(HandleUpdatedHighScoreFetched);
    }

    private void HandleUpdatedHighScoreFetched(OnUpdatedHighScoreFetched e)
    {
        _score.text = e.Score.ToString();
        _highScore.text = e.HighScore.ToString();

        _scoreSection.SetActive(true);
        _loadingSpinner.SetActive(false);
    }

    public void InitGameOverPanelText(bool win)
    {
        if (win) _gameOverText.text = "You Win!";
        else _gameOverText.text = "You Lose!";
    }
}