using UnityEngine;

public class SpaceshipDestroyEffectController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
