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
        EventBus.Subscribe<MenuSceneLoaded>(SendVerifyTokenRequest);
        EventBus.Subscribe<LoginFormLoginButtonClickEvent>(SendLoginRequest);
        EventBus.Subscribe<SignUpFormSignUpButtonClickEvent>(SendSignUpRequest);
        EventBus.Subscribe<MainMenuLogoutButtonClickEvent>(HandleLogoutRequest);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<MenuSceneLoaded>(SendVerifyTokenRequest);
        EventBus.Unsubscribe<LoginFormLoginButtonClickEvent>(SendLoginRequest);
        EventBus.Unsubscribe<SignUpFormSignUpButtonClickEvent>(SendSignUpRequest);
        EventBus.Unsubscribe<MainMenuLogoutButtonClickEvent>(HandleLogoutRequest);
    }

    private void SendVerifyTokenRequest(MenuSceneLoaded e)
    {
        if (_accessToken == null || _accessToken == string.Empty)
        {
            EventBus.Publish(new OnAuthMenuVerifyTokenFailedEvent());
            return;
        }
        StartCoroutine(ApiClient.Post<string, AuthErrorResponse>($"{_baseUrl}/verify", string.Empty, HandleVerifyToken, _accessToken));
    }

    private void SendLoginRequest(LoginFormLoginButtonClickEvent e)
    {
        var loginReq = new LoginRequest { email = e.Email, password = e.Password };
        StartCoroutine(ApiClient.Post<AuthSuccessResponse, AuthErrorResponse>($"{_baseUrl}/login", loginReq, HandleLoginRequest));
    }

    private void SendSignUpRequest(SignUpFormSignUpButtonClickEvent e)
    {
        var signUpReq = new SignUpRequest { name = e.Name, email = e.Email, password = e.Password };
        StartCoroutine(ApiClient.Post<AuthSuccessResponse, AuthErrorResponse>($"{_baseUrl}/signup", signUpReq, HandleSignUpRequest));
    }

    private void SendRefreshTokenRequest()
    {
        StartCoroutine(ApiClient.Post<AuthSuccessResponse, AuthErrorResponse>($"{_baseUrl}/refresh", string.Empty, HandleRefreshToken, _refreshToken));
    }

    private void HandleLogoutRequest(MainMenuLogoutButtonClickEvent e)
    {
        _accessToken = null;
        _refreshToken = null;

        PlayerPrefs.DeleteKey(Constants.ACCESS_TOKEN);
        PlayerPrefs.DeleteKey(Constants.REFRESH_TOKEN);
        PlayerPrefs.Save();

        EventBus.Publish(new OnLogoutSuccessEvent("Logout Successful."));
    }

    private void HandleLoginRequest(AuthSuccessResponse res, AuthErrorResponse error)
    {
        if (res != null)
        {
            UpdateTokens(res.accessToken, res.refreshToken);
            EventBus.Publish(new OnLoginSuccessEvent("Login Successful."));
        }
        else
        {
            EventBus.Publish(new OnLoginFailedEvent(error.message));
        }
    }

    private void HandleSignUpRequest(AuthSuccessResponse res, AuthErrorResponse error)
    {
        if (res != null)
        {
            UpdateTokens(res.accessToken, res.refreshToken);
            EventBus.Publish(new OnSignUpSuccessEvent("Sign Up Successful."));
        }
        else
        {
            EventBus.Publish(new OnSignUpFailedEvent(error.message));
        }
    }

    private void HandleVerifyToken(string success, AuthErrorResponse error)
    {
        if (error == null)
        {
            EventBus.Publish(new OnAuthMenuVerifyTokenSuccessEvent());
        }
        else
        {
            if ((error.code == "TOKEN_EXPIRED" || error.code == "INVALID_ID_TOKEN") && _refreshToken != null && _refreshToken != string.Empty)
            {
                SendRefreshTokenRequest();
            }
            else
            {
                EventBus.Publish(new OnAuthMenuVerifyTokenFailedEvent());
            }
        }
    }

    private void HandleRefreshToken(AuthSuccessResponse authResponse, AuthErrorResponse error)
    {
        if (authResponse != null)
        {
            UpdateTokens(authResponse.accessToken, authResponse.refreshToken);
            EventBus.Publish(new OnAuthMenuVerifyTokenSuccessEvent());
        }
        else
        {
            EventBus.Publish(new OnAuthMenuVerifyTokenFailedEvent());
        }
    }

    private void UpdateTokens(string accessToken, string refreshToken)
    {
        _accessToken = accessToken;
        _refreshToken = refreshToken;

        PlayerPrefs.SetString(Constants.ACCESS_TOKEN, accessToken);
        PlayerPrefs.SetString(Constants.REFRESH_TOKEN, refreshToken);
        PlayerPrefs.Save();
    }
}

