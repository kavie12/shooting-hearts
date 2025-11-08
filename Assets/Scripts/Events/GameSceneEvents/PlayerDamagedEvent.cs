using UnityEngine;

public sealed class PlayerDamagedEvent : EventData
{
    public Vector3 Position { get; }
    public int DamageAmount { get; }
    public PlayerDamagedEvent(Vector3 position, int damageAmount)
    {
        Position = position;
        DamageAmount = damageAmount;
    }
}