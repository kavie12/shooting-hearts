using System.Collections.Generic;
using UnityEngine;

// Manage all visual effects in the game
public class FxManager : MonoBehaviour
{
    // Singleton for DontDestroyOnLoad (Persistance across the scenes)
    private static FxManager Instance;

    private GameObject _fxPlayerDamage;
    private GameObject _fxPlayerDestroy;

    private Dictionary<EnemyType, GameObject> _fxEnemyDestroy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnGameStarted>(HandleGameStarted);
        EventBus.Subscribe<OnLevelLoaded>(HandleLevelLoaded);

        EventBus.Subscribe<OnPlayerDamaged>(HandlePlayerDamaged);
        EventBus.Subscribe<OnPlayerDestroyed>(HandlePlayerDestroyed);
        EventBus.Subscribe<OnEnemyDestroyed>(HandleEnemyDestroyed);
    }

    private void OnDisable()
    {
        EventBus.Subscribe<OnGameStarted>(HandleGameStarted);
        EventBus.Unsubscribe<OnLevelLoaded>(HandleLevelLoaded);

        EventBus.Unsubscribe<OnPlayerDamaged>(HandlePlayerDamaged);
        EventBus.Unsubscribe<OnPlayerDestroyed>(HandlePlayerDestroyed);
        EventBus.Unsubscribe<OnEnemyDestroyed>(HandleEnemyDestroyed);
    }

    private void HandleGameStarted(OnGameStarted e)
    {
        _fxPlayerDamage = e.GameConfig.PlayerConfig.FxDamage;
        _fxPlayerDestroy = e.GameConfig.PlayerConfig.FxDestroy;
    }

    private void HandleLevelLoaded(OnLevelLoaded e)
    {
        _fxEnemyDestroy ??= new Dictionary<EnemyType, GameObject>();
        _fxEnemyDestroy.Clear();

        foreach (var enemyConfig in e.LevelConfig.EnemyConfigs)
        {
            _fxEnemyDestroy.Add(enemyConfig.EnemyType, enemyConfig.FxDestroy);
        }
    }

    private void HandlePlayerDamaged(OnPlayerDamaged e)
    {
        var fx = Instantiate(_fxPlayerDamage, e.Position, transform.rotation);
        Destroy(fx, 4f);
    }

    private void HandlePlayerDestroyed(OnPlayerDestroyed e)
    {
        var fx = Instantiate(_fxPlayerDestroy, e.Position, transform.rotation);
        Destroy(fx, 4f);
    }

    private void HandleEnemyDestroyed(OnEnemyDestroyed e)
    {
        var fx = Instantiate(_fxEnemyDestroy[e.EnemyType], e.Position, transform.rotation);
        Destroy(fx, 3f);
    }
}