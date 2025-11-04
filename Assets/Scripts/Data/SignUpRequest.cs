[System.Serializable]
public class SignUpRequest
{
    public string name;
    public string email;
    public string password;

    public SignUpRequest(string name, string email, string password)
    {
        this.name = name;
        this.email = email;
        this.password = password;
    }
}