public sealed class OnLogoutSuccessEvent : EventData
{
    public string Message { get; }
    public OnLogoutSuccessEvent(string message)
    {
        Message = message;
    }
}