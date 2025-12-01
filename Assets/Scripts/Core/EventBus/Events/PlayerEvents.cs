using UnityEngine;

// Event data for when the player takes damage.
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

// Event data for when the player gets destroyed.
public sealed class OnPlayerDestroyed : IEventData
{
    public Vector2 Position { get; }
    public OnPlayerDestroyed(Vector2 position)
    {
        Position = position;
    }
}

// Event data for when the player health is changed.
public sealed class OnPlayerHealthUpdated : IEventData
{
    public int NewHealth { get; }
    public OnPlayerHealthUpdated(int newHealth)
    {
        NewHealth = newHealth;
    }
}

// Event data for when the player health is over.
public sealed class OnPlayerHealthOver : IEventData { }

// Event data for when the player shoots a bullet.
public sealed class OnPlayerShoot : IEventData { }