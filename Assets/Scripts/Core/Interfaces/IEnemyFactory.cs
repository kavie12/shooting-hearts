using UnityEngine;

public interface IEnemyFactory
{
    GameObject CreateEnemy(EnemyType enemyType);
    void ReleaseEnemy(GameObject enemy);
}