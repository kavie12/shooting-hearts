using UnityEngine;

[System.Serializable]
public class EnemyConfig
{
    public EnemyType Type;
    public GameObject Prefab;
    public float FallSpeed;
    public float SpawnOverTime;
    public int PoolSize;
}