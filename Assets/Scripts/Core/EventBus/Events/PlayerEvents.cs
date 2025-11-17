using UnityEngine;

public sealed class OnPlayerDamaged : IEventData
{
    public Vector2 Position { get; }
    public int DamageAmount { get; }
    public OnPlayerDamaged(Vector2 position, int damageAmount)
    {
        Position = position;
        DamageAmount = damageAmount;
    }
}

public sealed class OnPlayerDestroyed : IEventData
{
    public Vector2 Position { get; }
    public OnPlayerDestroyed(Vector2 position)
    {
        Position = position;
    }
}

public sealed class OnPlayerHealthUpdated : IEventData
{
    public int NewHealth { get; }
    public OnPlayerHealthUpdated(int newHealth)
    {
        NewHealth = newHealth;
    }
}

public sealed class OnPlayerHealthOver : IEventData { }

public sealed class OnPlayerShoot : IEventData { }