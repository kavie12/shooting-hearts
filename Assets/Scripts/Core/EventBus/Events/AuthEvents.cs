// Used when a login request is completed.
public sealed class OnLoginRequestCompleted : IEventData
{
    public bool Success { get; }
    public string Message { get; }
    public OnLoginRequestCompleted(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}

// Used when a signup request is completed.
public sealed class OnSignUpRequestCompleted : IEventData
{
    public bool Success { get; }
    public string Message { get; }
    public OnSignUpRequestCompleted(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}

// Used when a logout request is completed.
public sealed class OnLogoutRequestCompleted : IEventData
{
    public bool Success { get; }
    public string Message { get; }
    public OnLogoutRequestCompleted(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}

// Used when a token authentication request is completed.
public class OnTokenAuthenticationRequestCompleted : IEventData
{
    public bool Success { get; }
    public OnTokenAuthenticationRequestCompleted(bool success)
    {
        Success = success;
    }
}

// Used to request a reset password email.
public class OnResetPasswordEmailRequested : IEventData
{
    public string Email { get; }
    public OnResetPasswordEmailRequested(string email)
    {
        Email = email;
    }
}

// Used when a reset password email request is completed.
public class OnResetPasswordEmailRequestCompleted : IEventData
{
    public bool Success { get; }
    public string Message { get; }
    public OnResetPasswordEmailRequestCompleted(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}