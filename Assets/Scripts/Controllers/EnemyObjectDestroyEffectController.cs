using UnityEngine;

public class EnemyObjectDestroyEffectController : MonoBehaviour
{
    private ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_ps.isPlaying && gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
