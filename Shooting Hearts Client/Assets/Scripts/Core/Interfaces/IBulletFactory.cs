using UnityEngine;

// Factory interface for creating and releasing bullet objects.
public interface IBulletFactory
{
    GameObject CreateBullet();
    void ReleaseBullet(GameObject bullet);
}