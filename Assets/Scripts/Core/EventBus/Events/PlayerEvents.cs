using UnityEngine;

public sealed class PlayerDamagedEvent : IEventData
{
    public Vector3 Position { get; }
    public int DamageAmount { get; }
    public PlayerDamagedEvent(Vector3 position, int damageAmount)
    {
        Position = position;
        DamageAmount = damageAmount;
    }
}

public sealed class PlayerDestroyedEvent : IEventData
{
    public Vector3 Position;
    public PlayerDestroyedEvent(Vector3 position)
    {
        this.Position = position;
    }
}

public sealed class PlayerHealthUpdatedEvent : IEventData
{
    public int NewHealth { get; }
    public PlayerHealthUpdatedEvent(int newHealth)
    {
        NewHealth = newHealth;
    }
}

public sealed class PlayerHealthOverEvent : IEventData { }

public sealed class PlayerShootEvent : IEventData { }