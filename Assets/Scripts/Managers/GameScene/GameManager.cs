using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Subscribe<PlayerDiedEvent>(HandlePlayerDied);
        EventBus.Subscribe<BonusChanceGrantedEvent>(HandleBonusChanceGranted);
        EventBus.Subscribe<BonusChanceDeniedEvent>(HandleBonusChanceDeniedEvent);
        EventBus.Subscribe<AllLevelsCompletedEvent>(HandleAllLevelsCompleted);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerDiedEvent>(HandlePlayerDied);
        EventBus.Unsubscribe<BonusChanceGrantedEvent>(HandleBonusChanceGranted);
        EventBus.Unsubscribe<BonusChanceDeniedEvent>(HandleBonusChanceDeniedEvent);
        EventBus.Unsubscribe<AllLevelsCompletedEvent>(HandleAllLevelsCompleted);
    }

    private void Start()
    {
        EventBus.Publish(new GameStartEvent());
    }

    private void HandleAllLevelsCompleted(AllLevelsCompletedEvent e)
    {
        EventBus.Publish(new GameOverEvent());
    }

    private void HandlePlayerDied(PlayerDiedEvent e)
    {
        EventBus.Publish(new GameStopEvent());
    }

    private void HandleBonusChanceGranted(BonusChanceGrantedEvent e)
    {
        EventBus.Publish(new GameStartEvent());
    }

    private void HandleBonusChanceDeniedEvent(BonusChanceDeniedEvent e)
    {
        EventBus.Publish(new GameOverEvent());
    }
}