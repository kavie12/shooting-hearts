public sealed class OnSignUpSuccessEvent : EventData
{
    public string Message { get; }
    public OnSignUpSuccessEvent(string message)
    {
        Message = message;
    }
}