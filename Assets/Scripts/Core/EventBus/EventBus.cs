using System;
using System.Collections.Generic;

// Simple event bus implementation for publishing and subscribing to events
// Reference: https://www.youtube.com/watch?v=oJtIIDcImhU
public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> _assignedActions = new();

    public static void Publish(IEventData data)
    {
        Type type = data.GetType();
        if (_assignedActions.TryGetValue(type, out Delegate existingAction))
        {
            existingAction?.DynamicInvoke(data);
        }
    }

    public static void Subscribe<T>(Action<T> action) where T : IEventData
    {
        Type type = typeof(T);

        if (_assignedActions.ContainsKey(type))
        {
            _assignedActions[type] = Delegate.Combine(_assignedActions[type], action);
        }
        else
        {
            _assignedActions[type] = action;
        }
    }

    public static void Unsubscribe<T>(Action<T> action) where T : IEventData
    {
        Type type = typeof(T);

        if (_assignedActions.ContainsKey(type))
        {
            _assignedActions[type] = Delegate.Remove(_assignedActions[type], action);
        }
    }
}