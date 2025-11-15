using UnityEngine;

public class AuthManager : MonoBehaviour, IAuthProvider
{
    private static AuthManager instance;

    private readonly string _baseUrl = "http://localhost:3000/api/auth";
    private string _accessToken;
    private string _refreshToken;

    public string AccessToken => _accessToken;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnTokenAuthenticationRequest>(SendTokenAuthRequest);
        EventBus.Unsubscribe<OnLoginRequest>(SendLoginRequest);
        EventBus.Unsubscribe<OnSignUpRequest>(SendSignUpRequest);
        EventBus.Unsubscribe<OnLogoutRequest>(HandleLogoutRequest);
    }

    #region Send Requests

    private void SendTokenAuthRequest(OnTokenAuthenticationRequest e)
    {
        if (_accessToken == null || _accessToken == string.Empty)
        {
            EventBus.Publish(new OnTokenAuthenticationRequestComplete(false));
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
        var loginReq = new LoginRequest { email = e.Email, password = e.Password };
        StartCoroutine(ApiClient.Post<AuthSuccessResponse, AuthErrorResponse>($"{_baseUrl}/login", loginReq, HandleLoginRequest));
    }

    private void SendSignUpRequest(OnSignUpRequest e)
    {
        var signUpReq = new SignUpRequest { name = e.Name, email = e.Email, password = e.Password };
        StartCoroutine(ApiClient.Post<AuthSuccessResponse, AuthErrorResponse>($"{_baseUrl}/signup", signUpReq, HandleSignUpRequest));
    }

    #endregion

    #region Handle Request Completions

    private void HandleTokenAuthRequestComplete(string success, AuthErrorResponse error)
    {
        if (error == null)
        {
            EventBus.Publish(new OnTokenAuthenticationRequestComplete(true));
        }
        else
        {
            if ((error.code == "TOKEN_EXPIRED" || error.code == "INVALID_ID_TOKEN") && _refreshToken != null && _refreshToken != string.Empty)
            {
                SendRefreshTokenRequest();
            }
            else
            {
                EventBus.Publish(new OnTokenAuthenticationRequestComplete(false));
            }
        }
    }

    private void HandleRefreshTokenRequestComplete(AuthSuccessResponse authResponse, AuthErrorResponse error)
    {
        if (authResponse != null)
        {
            SaveAuthTokens(authResponse.accessToken, authResponse.refreshToken);
            EventBus.Publish(new OnTokenAuthenticationRequestComplete(true));
        }
        else
        {
            EventBus.Publish(new OnTokenAuthenticationRequestComplete(false));
        }
    }

    private void HandleLoginRequest(AuthSuccessResponse res, AuthErrorResponse error)
    {
        if (res != null)
        {
            SaveAuthTokens(res.accessToken, res.refreshToken);
            EventBus.Publish(new OnLoginRequestComplete(true, "Login Successful."));
        }
        else
        {
            EventBus.Publish(new OnLoginRequestComplete(false, error.message));
        }
    }

    private void HandleSignUpRequest(AuthSuccessResponse res, AuthErrorResponse error)
    {
        if (res != null)
        {
            SaveAuthTokens(res.accessToken, res.refreshToken);
            EventBus.Publish(new OnSignUpRequestComplete(true, "Sign Up Successful."));
        }
        else
        {
            EventBus.Publish(new OnSignUpRequestComplete(false, error.message));
        }
    }

    private void HandleLogoutRequest(OnLogoutRequest e)
    {
        _accessToken = null;
        _refreshToken = null;

        PlayerPrefs.DeleteKey(Constants.ACCESS_TOKEN);
        PlayerPrefs.DeleteKey(Constants.REFRESH_TOKEN);
        PlayerPrefs.Save();

        EventBus.Publish(new OnLogoutRequestComplete(true, "Logout Successful."));
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