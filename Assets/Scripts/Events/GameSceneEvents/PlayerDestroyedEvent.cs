using UnityEngine;

public sealed class PlayerDestroyedEvent : EventData
{
    public Vector3 Position;

    public PlayerDestroyedEvent(Vector3 position)
    {
        this.Position = position;
    }
}