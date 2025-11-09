using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig" ,menuName = "ScriptableObjects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public string LevelName;
    public float Duration;
    public string Hint;
    public EnemyConfig[] EnemyConfigs;
}