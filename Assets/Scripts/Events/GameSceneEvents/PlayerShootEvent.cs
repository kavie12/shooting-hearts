using UnityEngine;

public sealed class PlayerShootEvent : EventData
{
    public Vector3 Position { get; }

    public PlayerShootEvent(Vector3 position)
    {
        Position = position;
    }
}