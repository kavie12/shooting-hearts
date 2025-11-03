using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public string levelName;
    public EnemyObjectConfig[] enemyObjectConfigs;
}