public sealed class OnLoginFailedEvent : EventData
{
    public string Message { get; }
    public OnLoginFailedEvent(string message)
    {
        Message = message;
    }
}