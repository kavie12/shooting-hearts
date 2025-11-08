using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HeartGameAPIClient : MonoBehaviour
{
    private string url = "https://marcconrad.com/uob/heart/api.php";

    private void OnEnable()
    {
        EventBus.Subscribe<FetchBonusChanceQuestionEvent>(FetchQuestion);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<FetchBonusChanceQuestionEvent>(FetchQuestion);
    }

    public void FetchQuestion(FetchBonusChanceQuestionEvent e)
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
                EventBus.Publish(new BonusChanceQuestionFetchFailedEvent($"Error: {request.error}"));
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
                EventBus.Publish(new BonusChanceQuestionFetchSuccessEvent(new BonusChanceQuestion(texture, apiResponse.solution, apiResponse.carrots)));
            }
            else
            {
                EventBus.Publish(new BonusChanceQuestionFetchFailedEvent($"Error: {request.error}"));
            }
        }
    }
}