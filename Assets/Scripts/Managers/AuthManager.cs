using UnityEngine;

// Manage user authentication, including login, sign-up, token verification, and password reset.
public class AuthManager : MonoBehaviour, IAuthProvider
{
    // Singleton for DontDestroyOnLoad (Persistance across the scenes)
    private static AuthManager Instance;

    private readonly string _baseUrl = "http://localhost:3000/api/auth";
    private string _accessToken;
    private string _refreshToken;

    public string AccessToken => _accessToken;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        _accessToken = PlayerPrefs.GetString(Constants.ACCESS_TOKEN, string.Empty);
        _refreshToken = PlayerPrefs.GetString(Constants.REFRESH_TOKEN, string.Empty);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnTokenAuthenticationRequest>(SendTokenAuthRequest);
        EventBus.Subscribe<OnLoginRequest>(SendLoginRequest);
        EventBus.Subscribe<OnSignUpRequest>(SendSignUpRequest);
        EventBus.Subscribe<OnLogoutRequest>(HandleLogoutRequest);
        EventBus.Subscribe<OnResetPasswordEmailRequested>(SendResetPasswordEmailRequest);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnTokenAuthenticationRequest>(SendTokenAuthRequest);
        EventBus.Unsubscribe<OnLoginRequest>(SendLoginRequest);
        EventBus.Unsubscribe<OnSignUpRequest>(SendSignUpRequest);
        EventBus.Unsubscribe<OnLogoutRequest>(HandleLogoutRequest);
        EventBus.Unsubscribe<OnResetPasswordEmailRequested>(SendResetPasswordEmailRequest);
    }

    #region Send Requests

    private void SendTokenAuthRequest(OnTokenAuthenticationRequest e)
    {
        if (_accessToken == null || _accessToken == string.Empty)
        {
            EventBus.Publish(new OnTokenAuthenticationRequestCompleted(false));
            return;
        }
        StartCoroutine(ApiClient.Post<string, AuthErrorResponse>($"{_baseUrl}/verify", string.Empty, HandleTokenAuthRequestComplete, _accessToken));
    }

    private void SendRefreshTokenRequest()
    {
        StartCoroutine(ApiClient.Post<AuthSuccessResponse, AuthErrorResponse>($"{_baseUrl}/refresh", string.Empty, HandleRefreshTokenRequestComplete, _refreshToken));
    }

    private void SendLoginRequest(OnLoginRequest e)
    {
        var reqBody = new LoginRequest { email = e.Email, password = e.Password };
        StartCoroutine(ApiClient.Post<AuthSuccessResponse, AuthErrorResponse>($"{_baseUrl}/login", reqBody, HandleLoginRequest));
    }

    private void SendSignUpRequest(OnSignUpRequest e)
    {
        var reqBody = new SignUpRequest { name = e.Name, email = e.Email, password = e.Password };
        StartCoroutine(ApiClient.Post<AuthSuccessResponse, AuthErrorResponse>($"{_baseUrl}/signup", reqBody, HandleSignUpRequest));
    }

    private void SendResetPasswordEmailRequest(OnResetPasswordEmailRequested e)
    {
        var reqBody = new ResetPasswordEmailRequest { email = e.Email };
        StartCoroutine(ApiClient.Post<ResetPasswordEmailResponse, AuthErrorResponse>($"{_baseUrl}/reset-password-email", reqBody, HandleResetPasswordEmailRequest));
    }

    #endregion

    #region Handle Request Completions

    private void HandleTokenAuthRequestComplete(string success, AuthErrorResponse error)
    {
        if (error == null)
        {
            EventBus.Publish(new OnTokenAuthenticationRequestCompleted(true));
        }
        else
        {
            if ((error.code == "TOKEN_EXPIRED" || error.code == "INVALID_ID_TOKEN") && _refreshToken != null && _refreshToken != string.Empty)
            {
                SendRefreshTokenRequest();
            }
            else
            {
                EventBus.Publish(new OnTokenAuthenticationRequestCompleted(false));
            }
        }
    }

    private void HandleRefreshTokenRequestComplete(AuthSuccessResponse authResponse, AuthErrorResponse error)
    {
        if (authResponse != null)
        {
            SaveAuthTokens(authResponse.accessToken, authResponse.refreshToken);
            EventBus.Publish(new OnTokenAuthenticationRequestCompleted(true));
        }
        else
        {
            EventBus.Publish(new OnTokenAuthenticationRequestCompleted(false));
        }
    }

    private void HandleLoginRequest(AuthSuccessResponse res, AuthErrorResponse error)
    {
        if (res != null)
        {
            SaveAuthTokens(res.accessToken, res.refreshToken);
            EventBus.Publish(new OnLoginRequestCompleted(true, "Login Successful."));
        }
        else
        {
            EventBus.Publish(new OnLoginRequestCompleted(false, error.message));
        }
    }

    private void HandleSignUpRequest(AuthSuccessResponse res, AuthErrorResponse error)
    {
        if (res != null)
        {
            SaveAuthTokens(res.accessToken, res.refreshToken);
            EventBus.Publish(new OnSignUpRequestCompleted(true, "Sign Up Successful."));
        }
        else
        {
            EventBus.Publish(new OnSignUpRequestCompleted(false, error.message));
        }
    }

    private void HandleLogoutRequest(OnLogoutRequest e)
    {
        _accessToken = null;
        _refreshToken = null;

        PlayerPrefs.DeleteKey(Constants.ACCESS_TOKEN);
        PlayerPrefs.DeleteKey(Constants.REFRESH_TOKEN);
        PlayerPrefs.Save();

        EventBus.Publish(new OnLogoutRequestCompleted(true, "Logout Successful."));
    }

    private void HandleResetPasswordEmailRequest(ResetPasswordEmailResponse res, AuthErrorResponse error)
    {
        if (res != null)
        {
            EventBus.Publish(new OnResetPasswordEmailRequestCompleted(true, "Password reset email sent."));
        }
        else
        {
            EventBus.Publish(new OnResetPasswordEmailRequestCompleted(false, error.message));
        }
    }

    #endregion

    private void SaveAuthTokens(string accessToken, string refreshToken)
    {
        _accessToken = accessToken;
        _refreshToken = refreshToken;

        PlayerPrefs.SetString(Constants.ACCESS_TOKEN, accessToken);
        PlayerPrefs.SetString(Constants.REFRESH_TOKEN, refreshToken);
        PlayerPrefs.Save();
    }
}