using UnityEngine;

[System.Serializable]
public class EnemyConfig
{
    public EnemyType EnemyType;
    public GameObject Prefab;
    public GameObject FxDestroy;
    public float FallSpeed;
    public float SpawnOverTime;
    public float SpawnDelay;
    public int PoolSize;
}