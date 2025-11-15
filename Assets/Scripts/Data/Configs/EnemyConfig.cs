using System;
using UnityEngine;

[Serializable]
public class EnemyConfig
{
    public EnemyType EnemyType;
    public GameObject Prefab;
    public GameObject DestroyEffectPrefab;
    public float FallSpeed;
    public float SpawnOverTime;
    public float SpawnDelay;
    public int PoolSize;
}