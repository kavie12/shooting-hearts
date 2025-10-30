using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HeartGameAPIClient : MonoBehaviour
{
    public static HeartGameAPIClient Instance;

    public static event Action<HeartGameQuestion> OnQuestionFetched;
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
                HeartGameAPIResponse response = JsonUtility.FromJson<HeartGameAPIResponse>(request.downloadHandler.text);
                yield return StartCoroutine(FetchImageTextureCoroutine(response));
            }
            else
            {
                OnQuestionFetchFailed?.Invoke($"Error: {request.error}");
            }
        }
    }

    IEnumerator FetchImageTextureCoroutine(HeartGameAPIResponse apiResponse)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(apiResponse.question))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                OnQuestionFetched?.Invoke(new HeartGameQuestion(texture, apiResponse.solution, apiResponse.carrots));
            }
            else
            {
                OnQuestionFetchFailed?.Invoke($"Error: {request.error}");
            }
        }
    }
}
