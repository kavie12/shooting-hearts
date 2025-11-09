using UnityEngine;

public sealed class  EnemyDestroyedEvent : IEventData
{
    public EnemyType EnemyType { get; }
    public Vector3 Position { get; }
    public int PointsEarned { get; }

    public EnemyDestroyedEvent(EnemyType enemyType, Vector3 position, int pointsEarned)
    {
        EnemyType = enemyType;
        Position = position;
        PointsEarned = pointsEarned;
    }
}