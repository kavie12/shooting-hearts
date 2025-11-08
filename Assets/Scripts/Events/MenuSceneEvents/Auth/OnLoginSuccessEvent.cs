public sealed class OnLoginSuccessEvent : EventData
{
    public string Message { get; }
    public OnLoginSuccessEvent(string message)
    {
        Message = message;
    }
}