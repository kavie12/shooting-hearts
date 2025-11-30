public sealed class OnLoginRequestComplete : IEventData
{
    public bool Success { get; }
    public string Message { get; }
    public OnLoginRequestComplete(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}

public sealed class OnSignUpRequestComplete : IEventData
{
    public bool Success { get; }
    public string Message { get; }
    public OnSignUpRequestComplete(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}

public sealed class OnLogoutRequestComplete : IEventData
{
    public bool Success { get; }
    public string Message { get; }
    public OnLogoutRequestComplete(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}

public class OnTokenAuthenticationRequestComplete : IEventData
{
    public bool Success { get; }
    public OnTokenAuthenticationRequestComplete(bool success)
    {
        Success = success;
    }
}

public class OnResetPasswordEmailRequest : IEventData
{
    public string Email { get; }
    public OnResetPasswordEmailRequest(string email)
    {
        Email = email;
    }
}

public class OnResetPasswordEmailRequestComplete : IEventData
{
    public bool Success { get; }
    public string Message { get; }
    public OnResetPasswordEmailRequestComplete(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}