using UnityEngine;

public interface IBulletFactory
{
    GameObject CreateBullet();
    void ReleaseBullet(GameObject bullet);
}