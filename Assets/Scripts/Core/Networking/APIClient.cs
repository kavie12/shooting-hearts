using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// Simple API client for making HTTP requests using UnityWebRequest
public static class ApiClient
{
    public static IEnumerator Post<T1, T2>(string url, object body, Action<T1, T2> callback, string token = null)
    {
        string json = JsonUtility.ToJson(body);
        using UnityWebRequest req = UnityWebRequest.Post(url, json, "application/json");
        req.timeout = 10;
        if (token != null && token != string.Empty)
        {
            req.SetRequestHeader("Authorization", "Bearer " + token);
        }

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            callback(JsonUtility.FromJson<T1>(req.downloadHandler.text), default);
        }
        else
        {
            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Connection error.");
                yield break;
            }
            callback(default, JsonUtility.FromJson<T2>(req.downloadHandler.text));
        }
    }

    public static IEnumerator Get<T1, T2>(string url, Action<T1, T2> callback, string token = null)
    {
        using UnityWebRequest req = UnityWebRequest.Get(url);
        req.timeout = 10;
        if (token != null && token != string.Empty)
        {
            req.SetRequestHeader("Authorization", "Bearer " + token);
        }

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            callback(JsonUtility.FromJson<T1>(req.downloadHandler.text), default);
        }
        else
        {
            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Connection error.");
                yield break;
            }
            callback(default, JsonUtility.FromJson<T2>(req.downloadHandler.text));
        }
    }
}