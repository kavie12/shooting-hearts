using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HeartGameAPIClient : MonoBehaviour
{
    public static HeartGameAPIClient Instance;

    public static event Action<HeartGameAPIQuestion> OnQuestionFetched;
    public static event Action<string> OnQuestionFetchFailed;

    private string url = "https://marcconrad.com/uob/heart/api.php";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FetchQuestion()
    {
        StartCoroutine(FetchQuestionCoroutine());
    }

    IEnumerator FetchQuestionCoroutine()
    {
        using(UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    HeartGameAPIQuestion data = JsonUtility.FromJson<HeartGameAPIQuestion>(request.downloadHandler.text);
                    OnQuestionFetched?.Invoke(data);
                }
                catch (Exception e)
                {
                    OnQuestionFetchFailed?.Invoke($"JSON parse error: {e.Message}");
                }
            }
            else
            {
                OnQuestionFetchFailed?.Invoke($"Network error: {request.error}");
            }
        }
    }
}
