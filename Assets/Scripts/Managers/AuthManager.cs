using System;
using System.Collections;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{
    public static event Action OnSignUpSuccess;
    public static event Action OnLoginSuccess;

    private readonly string _baseUrl = "http://localhost:3000/api/auth";
    private string _token;

    private void OnEnable()
    {
        MenuController.OnLoginRequest += HandleLoginRequest;
        MenuController.OnSignUpRequest += HandleSignUpRequest;
    }

    private void OnDisable()
    {
        MenuController.OnLoginRequest -= HandleLoginRequest;
        MenuController.OnSignUpRequest -= HandleSignUpRequest;
    }

    public void HandleLoginRequest(LoginRequest loginRequest)
    {
        StartCoroutine(Login(loginRequest));
    }

    public void HandleSignUpRequest(SignUpRequest signUpRequest)
    {
        StartCoroutine(SignUp(signUpRequest));
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
                OnLoginSuccess?.Invoke();
            }
            else
            {
                Debug.LogError(req.downloadHandler.text);
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
                OnSignUpSuccess?.Invoke();
            }
            else
            {
                Debug.LogError(req.downloadHandler.text);
            }
        }
    }
}