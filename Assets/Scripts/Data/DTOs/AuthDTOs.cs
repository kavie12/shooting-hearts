[System.Serializable]
public class LoginRequest
{
    public string email;
    public string password;
}

[System.Serializable]
public class SignUpRequest
{
    public string name;
    public string email;
    public string password;
}

[System.Serializable]
public class AuthSuccessResponse
{
    public string accessToken;
    public string refreshToken;
}

[System.Serializable]
public class AuthErrorResponse
{
    public string code;
    public string message;
}