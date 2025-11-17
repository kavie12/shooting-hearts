using UnityEngine;

public sealed class  OnEnemyDestroyed : IEventData
{
    public EnemyType EnemyType;
    public Vector2 Position;
    public int PointsEarned { get; }
    public OnEnemyDestroyed(EnemyType enemyType, Vector2 position, int pointsEarned)
    {
        EnemyType = enemyType;
        Position = position;
        PointsEarned = pointsEarned;
    }
}