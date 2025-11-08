using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundAmbientSfx;
    [SerializeField] private AudioSource _bulletSfx;
    [SerializeField] private AudioSource _playerDestroySfx;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        PlayBackgroundAmbientSoundEffect();
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

    private void PlayBackgroundAmbientSoundEffect()
    {
        _backgroundAmbientSfx.playOnAwake = true;
        _backgroundAmbientSfx.loop = true;
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