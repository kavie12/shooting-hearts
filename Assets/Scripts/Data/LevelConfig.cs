using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig" ,menuName = "ScriptableObjects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public string levelName;
    public float duration;
    public EnemyObjectSpawnConfig[] enemyObjectSpawnConfig;
}