// Used when a button is clicked in the Menu Scene UI, indicating which button in MenuSceneButton enum was clicked.
public sealed class OnMenuSceneButtonClick : IEventData
{
    public MenuSceneButton ButtonId { get; }
    public OnMenuSceneButtonClick(MenuSceneButton buttonId)
    {
        this.ButtonId = buttonId;
    }
}

// Used to request authentication using a stored token.
public sealed class OnTokenAuthenticationRequest : IEventData { }

// Used to request for login with email and password.
public sealed class OnLoginRequest : IEventData
{
    public string Email { get; }
    public string Password { get; }
    public OnLoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

// Used to request for signup with name, email and password.
public sealed class OnSignUpRequest : IEventData
{
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }
    public OnSignUpRequest(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}

// Used to request for logout.
public sealed class OnLogoutRequest : IEventData { }