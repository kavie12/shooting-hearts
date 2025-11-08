using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour, IAuthTokenProvider
{
    private readonly string _baseUrl = "http://localhost:3000/api/auth";
    private string token;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<LoginFormLoginButtonClickEvent>(HandleLoginRequest);
        EventBus.Subscribe<SignUpFormSignUpButtonClickEvent>(HandleSignUpRequest);
        EventBus.Subscribe<MainMenuLogoutButtonClickEvent>(HandleLogoutRequest);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LoginFormLoginButtonClickEvent>(HandleLoginRequest);
        EventBus.Unsubscribe<SignUpFormSignUpButtonClickEvent>(HandleSignUpRequest);
        EventBus.Unsubscribe<MainMenuLogoutButtonClickEvent>(HandleLogoutRequest);
    }

    public string GetToken() {
        return token;
    }

    public void HandleLoginRequest(LoginFormLoginButtonClickEvent e)
    {
        StartCoroutine(SendLoginRequest(new LoginRequest { email = e.Email, password = e.Password }));
    }

    public void HandleSignUpRequest(SignUpFormSignUpButtonClickEvent e)
    {
        StartCoroutine(SendSignUpRequest(new SignUpRequest { name = e.Name, email = e.Email, password = e.Password }));
    }

    public void HandleLogoutRequest(MainMenuLogoutButtonClickEvent e)
    {
        token = null;
        EventBus.Publish(new OnLogoutSuccessEvent("Logout Successful."));
    }

    public IEnumerator SendLoginRequest(LoginRequest loginRequest)
    {
        string jsonData = JsonUtility.ToJson(loginRequest);

        using UnityWebRequest req = UnityWebRequest.Post($"{_baseUrl}/login", jsonData, "application/json");
        req.timeout = 10;
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            AuthSuccessResponse res = JsonUtility.FromJson<AuthSuccessResponse>(req.downloadHandler.text);
            token = res.token;
            EventBus.Publish(new OnLoginSuccessEvent("Login Successful."));
        }
        else
        {
            EventBus.Publish(new OnLoginFailedEvent(ParseError(req)));
        }
    }

    public IEnumerator SendSignUpRequest(SignUpRequest signUpRequest)
    {
        string jsonData = JsonUtility.ToJson(signUpRequest);

        using (UnityWebRequest req = UnityWebRequest.Post($"{_baseUrl}/signup", jsonData, "application/json"))
        {
            req.timeout = 10;
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                AuthSuccessResponse authResponse = JsonUtility.FromJson<AuthSuccessResponse>(req.downloadHandler.text);
                token = authResponse.token;
                EventBus.Publish(new OnSignUpSuccessEvent("Sign Up Successful."));
            }
            else
            {
                EventBus.Publish(new OnSignUpFailedEvent(ParseError(req)));
            }
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

