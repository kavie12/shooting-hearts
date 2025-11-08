public sealed class PlayerDamagedEvent : EventData
{
    public int DamageAmount { get; }
    public PlayerDamagedEvent(int damageAmount)
    {
        DamageAmount = damageAmount;
    }
}