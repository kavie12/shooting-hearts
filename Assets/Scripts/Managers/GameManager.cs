using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerDestroyedEvent>(HandlePlayerDestroyed);
        EventBus.Subscribe<BonusChanceGrantedEvent>(HandleBonusChanceGranted);
        EventBus.Subscribe<BonusChanceDeniedEvent>(HandleBonusChanceDeniedEvent);
        EventBus.Subscribe<AllLevelsCompletedEvent>(HandleAllLevelsCompleted);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerDestroyedEvent>(HandlePlayerDestroyed);
        EventBus.Unsubscribe<BonusChanceGrantedEvent>(HandleBonusChanceGranted);
        EventBus.Unsubscribe<BonusChanceDeniedEvent>(HandleBonusChanceDeniedEvent);
        EventBus.Unsubscribe<AllLevelsCompletedEvent>(HandleAllLevelsCompleted);
    }

    private void Start()
    {
        EventBus.Publish(new GameStartEvent(_gameConfig));
    }

    private void HandleAllLevelsCompleted(AllLevelsCompletedEvent e)
    {
        Invoke(nameof(InvokeGameOverEvent), 4f);
    }

    private void HandlePlayerDestroyed(PlayerDestroyedEvent e)
    {
        EventBus.Publish(new GameStopEvent());
    }

    private void HandleBonusChanceGranted(BonusChanceGrantedEvent e)
    {
        EventBus.Publish(new GameContinueEvent());
    }

    private void HandleBonusChanceDeniedEvent(BonusChanceDeniedEvent e)
    {
        InvokeGameOverEvent();
    }

    private void InvokeGameOverEvent()
    {
        EventBus.Publish(new GameOverEvent());
    }
}