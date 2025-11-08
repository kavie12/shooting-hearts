public sealed class OnSignUpFailedEvent : EventData
{
    public string Message { get; }
    public OnSignUpFailedEvent(string message)
    {
        Message = message;
    }
}