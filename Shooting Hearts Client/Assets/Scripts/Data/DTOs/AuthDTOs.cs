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

[System.Serializable]
public class ResetPasswordEmailRequest
{
    public string email;
}

[System.Serializable]
public class ResetPasswordEmailResponse
{
    public string message;
}

[System.Serializable]
public class ConfirmResetPasswordRequest
{
    public string code;
    public string newPassword;
}

[System.Serializable]
public class ConfirmResetPasswordResponse
{
    public string message;
}