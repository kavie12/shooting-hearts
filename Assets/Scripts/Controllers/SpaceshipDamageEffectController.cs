using UnityEngine;

public class SpaceshipDamageEffectController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
