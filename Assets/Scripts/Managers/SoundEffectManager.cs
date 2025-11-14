using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance;

    [SerializeField] private AudioSource _bulletSfx;
    [SerializeField] private AudioSource _playerDestroySfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerShootEvent>(PlayPlayerShootSoundEffect);
        EventBus.Subscribe<PlayerDamagedEvent>(PlayPlayerDamageSoundEffect);
        EventBus.Subscribe<PlayerDestroyedEvent>(PlayPlayerDestroySoundEffect);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerShootEvent>(PlayPlayerShootSoundEffect);
        EventBus.Unsubscribe<PlayerDamagedEvent>(PlayPlayerDamageSoundEffect);
        EventBus.Unsubscribe<PlayerDestroyedEvent>(PlayPlayerDestroySoundEffect);
    }

    private void PlayPlayerShootSoundEffect(PlayerShootEvent e)
    {
        _bulletSfx.Play();
    }

    private void PlayPlayerDamageSoundEffect(PlayerDamagedEvent e)
    {

    }

    private void PlayPlayerDestroySoundEffect(PlayerDestroyedEvent e)
    {
        _playerDestroySfx.Play();
    }
}