using System.Collections.Generic;
using UnityEngine;

// All the soundeffects in the game
public enum Sfx
{
    BackgroundAmbient,
    GameWin,
    GameLose,
    LevelUp,
    PlayerDamage,
    PlayerDestroy,
    PlayerShoot,
    PlayerScore
}

// Manage sound effects in the game, including playing appropriate sounds in response to game events.
public class SfxManager : MonoBehaviour
{
    // Singleton for DontDestroyOnLoad (Persistance across the scenes)
    private static SfxManager Instance;

    [SerializeField] private SfxAudioResource[] _audioResources;

    private Dictionary<Sfx, AudioSource> _audioSources;

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

    private void Start()
    {
        _audioSources = new Dictionary<Sfx, AudioSource>();

        foreach (var audioResource in _audioResources)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.resource = audioResource.AudioResource;
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            _audioSources.Add(audioResource.Sfx, audioSource);
        }

        _audioSources[Sfx.BackgroundAmbient].Play();
        _audioSources[Sfx.BackgroundAmbient].loop = true;
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnPlayerDamaged>(HandlePlayerDamaged);
        EventBus.Subscribe<OnPlayerDestroyed>(HandlePlayerDestroyed);
        EventBus.Subscribe<OnPlayerShoot>(HandlePlayerShoot);
        EventBus.Subscribe<OnEnemyDestroyed>(HandleEnemyDestroyed);
        EventBus.Subscribe<OnLevelStarted>(HandleLevelStarted);
        EventBus.Subscribe<OnGameOver>(HandleGameOver);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerDamaged>(HandlePlayerDamaged);
        EventBus.Unsubscribe<OnPlayerDestroyed>(HandlePlayerDestroyed);
        EventBus.Unsubscribe<OnPlayerShoot>(HandlePlayerShoot);
        EventBus.Unsubscribe<OnEnemyDestroyed>(HandleEnemyDestroyed);
        EventBus.Unsubscribe<OnLevelStarted>(HandleLevelStarted);
        EventBus.Unsubscribe<OnGameOver>(HandleGameOver);
    }

    private void HandlePlayerDamaged(OnPlayerDamaged e)
    {
        _audioSources[Sfx.PlayerDamage].Play();
    }

    private void HandlePlayerDestroyed(OnPlayerDestroyed e)
    {
        _audioSources[Sfx.PlayerDestroy].Play();
    }

    private void HandlePlayerShoot(OnPlayerShoot e)
    {
        _audioSources[Sfx.PlayerShoot].Play();
    }

    private void HandleEnemyDestroyed(OnEnemyDestroyed e)
    {
        _audioSources[Sfx.PlayerScore].Play();
    }

    private void HandleLevelStarted(OnLevelStarted e)
    {
        _audioSources[Sfx.LevelUp].Play();
    }

    private void HandleGameOver(OnGameOver e)
    {
        if (e.Win) _audioSources[Sfx.GameWin].Play();
        else _audioSources[Sfx.GameLose].Play();
    }
}