public sealed class MenuSceneLoaded : IEventData
{
}

public sealed class AuthMenuLoginButtonClickEvent : IEventData
{
}

public sealed class AuthMenuSignUpButtonClickEvent : IEventData
{
}

public sealed class LoginFormBackButtonClickEvent : IEventData
{
}

public sealed class LoginFormLoginButtonClickEvent : IEventData
{
    public string Email { get; }
    public string Password { get; }
    public LoginFormLoginButtonClickEvent(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public sealed class SignUpFormBackButtonClickEvent : IEventData
{
}

public sealed class SignUpFormSignUpButtonClickEvent : IEventData
{
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }
    public SignUpFormSignUpButtonClickEvent(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}

public sealed class MainMenuPlayButtonClickEvent : IEventData
{
}

public sealed class MainMenuLeaderboardButtonClickEvent : IEventData
{
}

public sealed class LeaderboardBackButtonClickEvent : IEventData
{
}

public sealed class MainMenuLogoutButtonClickEvent : IEventData
{
}

public sealed class MenuSceneExitButtonClickEvent : IEventData
{
}