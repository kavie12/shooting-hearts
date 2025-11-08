public sealed class LoginFormLoginButtonClickEvent : EventData
{
    public string Email { get; }
    public string Password { get; }

    public LoginFormLoginButtonClickEvent(string email, string password)
    {
        Email = email;
        Password = password;
    }
}