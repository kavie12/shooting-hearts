public interface IEnemy
{
    EnemyType EnemyType { get; }
    int Points { get; }
    int Damage { get; }

    void UpdateConfig(EnemyConfig enemyConfig);
}