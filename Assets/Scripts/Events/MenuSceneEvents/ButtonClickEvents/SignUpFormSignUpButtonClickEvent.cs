public sealed class SignUpFormSignUpButtonClickEvent : EventData
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