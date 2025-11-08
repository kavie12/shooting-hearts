using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour, IAuthTokenProvider
{
    private readonly string _baseUrl = "http://localhost:3000/api/auth";
    private string _token;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _token = PlayerPrefs.GetString("auth_token", string.Empty);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<MenuSceneLoaded>(VerifyToken);
        EventBus.Subscribe<LoginFormLoginButtonClickEvent>(HandleLoginRequest);
        EventBus.Subscribe<SignUpFormSignUpButtonClickEvent>(HandleSignUpRequest);
        EventBus.Subscribe<MainMenuLogoutButtonClickEvent>(HandleLogoutRequest);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<MenuSceneLoaded>(VerifyToken);
        EventBus.Unsubscribe<LoginFormLoginButtonClickEvent>(HandleLoginRequest);
        EventBus.Unsubscribe<SignUpFormSignUpButtonClickEvent>(HandleSignUpRequest);
        EventBus.Unsubscribe<MainMenuLogoutButtonClickEvent>(HandleLogoutRequest);
    }

    public string GetToken() => _token;

    private void HandleLoginRequest(LoginFormLoginButtonClickEvent e)
    {
        StartCoroutine(SendLoginRequest(new LoginRequest { email = e.Email, password = e.Password }));
    }

    private void HandleSignUpRequest(SignUpFormSignUpButtonClickEvent e)
    {
        StartCoroutine(SendSignUpRequest(new SignUpRequest { name = e.Name, email = e.Email, password = e.Password }));
    }

    private void VerifyToken(MenuSceneLoaded e)
    {
        if (_token == null || _token == string.Empty)
        {
            EventBus.Publish(new OnAuthMenuVerifyTokenFailedEvent());
            return;
        }
        StartCoroutine(SendVerifyTokenRequest(new VerifyTokenRequest { token = _token }));
    }

    private void HandleLogoutRequest(MainMenuLogoutButtonClickEvent e)
    {
        _token = null;
        PlayerPrefs.DeleteKey("auth_token");
        EventBus.Publish(new OnLogoutSuccessEvent("Logout Successful."));
    }

    private IEnumerator SendLoginRequest(LoginRequest loginRequest)
    {
        string jsonData = JsonUtility.ToJson(loginRequest);

        using UnityWebRequest req = UnityWebRequest.Post($"{_baseUrl}/login", jsonData, "application/json");
        req.timeout = 10;
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            AuthSuccessResponse res = JsonUtility.FromJson<AuthSuccessResponse>(req.downloadHandler.text);
            
            _token = res.token;

            PlayerPrefs.SetString("auth_token", _token);
            PlayerPrefs.Save();

            EventBus.Publish(new OnLoginSuccessEvent("Login Successful."));
        }
        else
        {
            EventBus.Publish(new OnLoginFailedEvent(ParseError(req)));
        }
    }

    private IEnumerator SendSignUpRequest(SignUpRequest signUpRequest)
    {
        string jsonData = JsonUtility.ToJson(signUpRequest);

        using (UnityWebRequest req = UnityWebRequest.Post($"{_baseUrl}/signup", jsonData, "application/json"))
        {
            req.timeout = 10;
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                AuthSuccessResponse authResponse = JsonUtility.FromJson<AuthSuccessResponse>(req.downloadHandler.text);

                _token = authResponse.token;

                PlayerPrefs.SetString("auth_token", _token);
                PlayerPrefs.Save();

                EventBus.Publish(new OnSignUpSuccessEvent("Sign Up Successful."));
            }
            else
            {
                EventBus.Publish(new OnSignUpFailedEvent(ParseError(req)));
            }
        }
    }

    private IEnumerator SendVerifyTokenRequest(VerifyTokenRequest verifyTokenRequest)
    {
        string jsonData = JsonUtility.ToJson(verifyTokenRequest);

        using UnityWebRequest req = UnityWebRequest.Post($"{_baseUrl}/verify-token", jsonData, "application/json");
        req.timeout = 10;
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            EventBus.Publish(new OnAuthMenuVerifyTokenSuccessEvent());
        }
        else
        {
            EventBus.Publish(new OnAuthMenuVerifyTokenFailedEvent());
        }
    }

    private string ParseError(UnityWebRequest req)
    {
        if (req.responseCode == 400)
        {
            return JsonUtility.FromJson<ErrorResponse>(req.downloadHandler.text).message;
        }
        else if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            return "Failed connection with server.";
        }
        return "An unexpected error occurred.";
    }
}

