using UnityEngine;

// Used when an enemy is destroyed, indicating its type, position, and points earned.
public sealed class  OnEnemyDestroyed : IEventData
{
    public EnemyType EnemyType { get; }
    public Vector2 Position { get; }
    public int PointsEarned { get; }
    public OnEnemyDestroyed(EnemyType enemyType, Vector2 position, int pointsEarned)
    {
        EnemyType = enemyType;
        Position = position;
        PointsEarned = pointsEarned;
    }
}