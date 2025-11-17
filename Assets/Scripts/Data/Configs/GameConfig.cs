using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
public class GameConfig : ScriptableObject
{
    public PlayerConfig PlayerConfig;
    public LevelConfig[] LevelConfigs;
}