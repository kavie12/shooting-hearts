public sealed class ShowAlertEvent : IEventData
{
    public string Message { get; }
    public float Duration { get; }

    public ShowAlertEvent(string message)
    {
        this.Message = message;
        Duration = 2f;
    }

    public ShowAlertEvent(string message, float duration)
    {
        this.Message = message;
        Duration = duration;
    }
}