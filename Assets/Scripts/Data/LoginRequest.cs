[System.Serializable]
public class LoginRequest
{
    public string email;
    public string password;

    public LoginRequest(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}