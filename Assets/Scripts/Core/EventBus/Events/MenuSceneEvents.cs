public sealed class OnMenuSceneButtonClick : IEventData
{
    public MenuSceneButton ButtonId { get; }
    public OnMenuSceneButtonClick(MenuSceneButton buttonId)
    {
        this.ButtonId = buttonId;
    }
}

public sealed class OnTokenAuthenticationRequest : IEventData { }

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

public sealed class OnLogoutRequest : IEventData { }