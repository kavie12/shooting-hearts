public sealed class  OnEnemyDestroyed : IEventData
{
    public int PointsEarned { get; }

    public OnEnemyDestroyed(int pointsEarned)
    {
        PointsEarned = pointsEarned;
    }
}