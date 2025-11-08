public sealed class PlayerHealthUpdatedEvent : EventData
{
    public int NewHealth { get; }
    
    public PlayerHealthUpdatedEvent(int newHealth)
    {
        NewHealth = newHealth;
    }
}