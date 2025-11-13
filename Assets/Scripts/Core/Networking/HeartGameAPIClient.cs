using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class HeartGameApiClient
{
    private static readonly string _apiUrl = "https://marcconrad.com/uob/heart/api.php";

    public static IEnumerator SendBonusChanceQuestionRequest(Action<BonusChanceQuestion, string> callback)
    {
        // Fetch question
        using UnityWebRequest questionReq = UnityWebRequest.Get(_apiUrl);
        questionReq.timeout = 10;
        yield return questionReq.SendWebRequest();

        if (questionReq.result == UnityWebRequest.Result.Success)
        {
            BonusChanceQuestionResponse question = JsonUtility.FromJson<BonusChanceQuestionResponse>(questionReq.downloadHandler.text);

            // Fetch image
            using UnityWebRequest imageReq = UnityWebRequestTexture.GetTexture(question.question);
            imageReq.timeout = 10;
            yield return imageReq.SendWebRequest();

            if (imageReq.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(imageReq);
                callback(new BonusChanceQuestion(texture, question.solution, question.carrots), null);
            }
            else
            {
                callback(null, "An unexpected error occurred.");
            }
        }
        else
        {
            callback(null, "An unexpected error occurred.");
        }
    }
}

