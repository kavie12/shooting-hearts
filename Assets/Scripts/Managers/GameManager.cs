using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    private bool _win = false;

    private void Start()
    {
        EventBus.Publish(new OnGameStarted(_gameConfig));
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnPlayerDestroyed>(HandlePlayerDestroyed);
        EventBus.Subscribe<OnBonusChanceRequestCompleted>(HandleBonusChanceRequestCompleted);
        EventBus.Subscribe<OnAllLevelsCompleted>(HandleAllLevelsCompleted);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerDestroyed>(HandlePlayerDestroyed);
        EventBus.Unsubscribe<OnBonusChanceRequestCompleted>(HandleBonusChanceRequestCompleted);
        EventBus.Unsubscribe<OnAllLevelsCompleted>(HandleAllLevelsCompleted);
    }

    private void HandlePlayerDestroyed(OnPlayerDestroyed e)
    {
        EventBus.Publish(new OnGameStopped());
        Invoke(nameof(RequestBonusChance), 3f);
    }

    private void RequestBonusChance()
    {
        EventBus.Publish(new OnBonusChanceRequested());
    }

    private void HandleBonusChanceRequestCompleted(OnBonusChanceRequestCompleted e)
    {
        if (e.Granted) EventBus.Publish(new OnGameContinued());
        else GameOver();
    }

    private void HandleAllLevelsCompleted(OnAllLevelsCompleted e)
    {
        _win = true;
        Invoke(nameof(GameOver), 4f);
    }

    private void GameOver()
    {
        EventBus.Publish(new OnGameOver(_win));
    }
}