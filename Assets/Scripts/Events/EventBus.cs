using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, Delegate> assignedActions = new();

    public static void Publish(EventData data)
    {
        Type type = data.GetType();
        if (assignedActions.TryGetValue(type, out Delegate existingAction))
        {
            existingAction?.DynamicInvoke(data);
        }
    }

    public static void Subscribe<T>(Action<T> action) where T : EventData
    {
        Type type = typeof(T);

        if (assignedActions.ContainsKey(type))
        {
            assignedActions[type] = Delegate.Combine(assignedActions[type], action);
        }
        else
        {
            assignedActions[type] = action;
        }
    }

    public static void Unsubscribe<T>(Action<T> action) where T : EventData
    {
        Type type = typeof(T);

        if (assignedActions.ContainsKey(type))
        {
            assignedActions[type] = Delegate.Remove(assignedActions[type], action);
        }
    }
}