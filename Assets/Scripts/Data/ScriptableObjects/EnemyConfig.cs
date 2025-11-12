using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public EnemyType EnemyType;
    public GameObject Prefab;
    public GameObject DestroyEffectPrefab;
    public float FallSpeed;
    public float SpawnOverTime;
    public float SpawnDelay;
    public int PoolSize;
}