// This interface defines the structure for enemy entities in the game.
public interface IEnemy
{
    EnemyType EnemyType { get; }
    int Points { get; }
    int Damage { get; }

    void UpdateConfig(EnemyConfig enemyConfig);
}