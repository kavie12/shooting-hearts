using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _highScore;
    [SerializeField] private Button _btnLogout;
    [SerializeField] private GameObject _loadingSpinner;

    private void Start()
    {
        _btnLogout.onClick.AddListener(() => EventBus.Publish(new OnLogoutRequest()));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnHighScoreRequestCompleted>(HandleHighScoreRequestComplete);

        SetLoading(true);
        EventBus.Publish(new OnHighScoreRequest());
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnHighScoreRequestCompleted>(HandleHighScoreRequestComplete);
    }

    private void HandleHighScoreRequestComplete(OnHighScoreRequestCompleted e)
    {
        if (e.Success)
        {
            _playerName.text = e.PlayerName;
            _highScore.text = "High Score: " + e.HighScore.ToString();

            SetLoading(false);
        }
        else
        {
            Debug.Log("Error fetching player high score.");
            _loadingSpinner.SetActive(false);
        }
    }

    private void SetLoading(bool set)
    {
        if (set)
        {
            _playerName.gameObject.SetActive(false);
            _highScore.gameObject.SetActive(false);
            _btnLogout.gameObject.SetActive(false);
            _loadingSpinner.SetActive(true);
        }
        else
        {
            _playerName.gameObject.SetActive(true);
            _highScore.gameObject.SetActive(true);
            _btnLogout.gameObject.SetActive(true);
            _loadingSpinner.SetActive(false);
        }
    }
}