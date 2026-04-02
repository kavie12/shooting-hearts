using UnityEngine;

// Factory interface for initializing, creating and releasing enemy game objects.
public interface IEnemyFactory
{
    void InitFactory(EnemyConfig[] enemyConfigs);
    GameObject CreateEnemy(EnemyType enemyType);
    void ReleaseEnemy(GameObject enemy);
}