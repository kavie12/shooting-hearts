public sealed class OnLoginSuccessEvent : IEventData
{
    public string Message { get; }
    public OnLoginSuccessEvent(string message)
    {
        Message = message;
    }
}

public sealed class OnLoginFailedEvent : IEventData
{
    public string Message { get; }
    public OnLoginFailedEvent(string message)
    {
        Message = message;
    }
}

public sealed class OnSignUpSuccessEvent : IEventData
{
    public string Message { get; }
    public OnSignUpSuccessEvent(string message)
    {
        Message = message;
    }
}

public sealed class OnSignUpFailedEvent : IEventData
{
    public string Message { get; }
    public OnSignUpFailedEvent(string message)
    {
        Message = message;
    }
}

public sealed class OnLogoutSuccessEvent : IEventData
{
    public string Message { get; }
    public OnLogoutSuccessEvent(string message)
    {
        Message = message;
    }
}

public class OnAuthMenuVerifyTokenSuccessEvent : IEventData { }

public class OnAuthMenuVerifyTokenFailedEvent : IEventData { }