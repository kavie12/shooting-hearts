using UnityEngine;

public interface IEnemyFactory
{
    void InitFactory(EnemyConfig[] enemyConfigs);
    GameObject CreateEnemy(EnemyType enemyType);
    void ReleaseEnemy(GameObject enemy);
}