using System;
using UnityEngine;

[Serializable]
public class EnemyObjectDestroyEffectPoolConfig
{
    public EnemyObject enemyObject;
    public GameObject prefab;
    public int poolSize = 1;
}