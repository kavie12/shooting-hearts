using System;
using UnityEngine;

[Serializable]
public class EnemyObjectConfig
{
    public EnemyObject enemyObject;

    [Header("Spawn Config")]
    public float spawnOverTime;
    public float spawnDelay;
    public float fallingSpeed;

    [Header("Pool Config")]
    public int enemyObjectPoolSize = 10;
    public int enemyObjectDestroyFXPoolSize = 5;
}