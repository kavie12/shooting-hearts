using System;
using UnityEngine;

[Serializable]
public class EnemyObjectPoolConfig
{
    public EnemyObject enemyObject;
    public GameObject prefab;
    public int poolSize;
}