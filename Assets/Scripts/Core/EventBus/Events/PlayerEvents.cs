public sealed class OnPlayerDamaged : IEventData
{
    public int DamageAmount { get; }
    public OnPlayerDamaged(int damageAmount)
    {
        DamageAmount = damageAmount;
    }
}

public sealed class OnPlayerDestroyed : IEventData { }

public sealed class OnPlayerHealthUpdated : IEventData
{
    public int NewHealth { get; }
    public OnPlayerHealthUpdated(int newHealth)
    {
        NewHealth = newHealth;
    }
}

public sealed class OnPlayerHealthOver : IEventData { }