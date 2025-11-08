public sealed class  EnemyDestroyedEvent : EventData
{
    public int PointsEarned { get; }

    public EnemyDestroyedEvent(int pointsEarned)
    {
        PointsEarned = pointsEarned;
    }
}