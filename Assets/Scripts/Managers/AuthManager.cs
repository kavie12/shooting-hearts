using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{
    public static event Action<string> OnSignUpSuccess;
    public static event Action<string> OnSignUpFailed;
    public static event Action<string> OnLoginSuccess;
    public static event Action<string> OnLoginFailed;

    private readonly string _baseUrl = "http://localhost:3000/api/auth";
    private string _token = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        LoginForm.OnLoginRequest += HandleLoginRequest;
        SignUpForm.OnSignUpRequest += HandleSignUpRequest;
        MainMenu.OnLogoutClicked += HandleLogoutRequest;
    }

    private void OnDisable()
    {
        LoginForm.OnLoginRequest -= HandleLoginRequest;
        SignUpForm.OnSignUpRequest -= HandleSignUpRequest;
        MainMenu.OnLogoutClicked -= HandleLogoutRequest;
    }

    public void HandleLoginRequest(LoginRequest loginRequest)
    {
        StartCoroutine(Login(loginRequest));
    }

    public void HandleSignUpRequest(SignUpRequest signUpRequest)
    {
        StartCoroutine(SignUp(signUpRequest));
    }

    public void HandleLogoutRequest()
    {
        _token = null;
    }

    public IEnumerator Login(LoginRequest loginRequest)
    {
        string jsonData = JsonUtility.ToJson(loginRequest);

        using (UnityWebRequest req = UnityWebRequest.Post($"{_baseUrl}/login", jsonData, "application/json"))
        {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(req.downloadHandler.text);
                _token = authResponse.data;
                OnLoginSuccess?.Invoke("Login Successful.");
            }
            else
            {
                AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(req.downloadHandler.text);
                OnLoginFailed?.Invoke(authResponse.data);
            }
        }
    }

    public IEnumerator SignUp(SignUpRequest signUpRequest)
    {
        string jsonData = JsonUtility.ToJson(signUpRequest);

        using (UnityWebRequest req = UnityWebRequest.Post($"{_baseUrl}/signup", jsonData, "application/json"))
        {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(req.downloadHandler.text);
                _token = authResponse.data;
                OnSignUpSuccess?.Invoke("Sign Up Successful.");
            }
            else
            {
                AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(req.downloadHandler.text);
                OnSignUpFailed?.Invoke(authResponse.data);
            }
        }
    }
}

[Serializable]
public class AuthResponse
{
    public string data;
}