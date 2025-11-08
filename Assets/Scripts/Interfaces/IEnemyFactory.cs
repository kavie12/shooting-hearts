using UnityEngine;

public interface IEnemyFactory
{
    GameObject CreateEnemy(EnemyType type);
    void ReleaseEnemy(GameObject enemy);
}