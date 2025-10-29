using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _score = 0;
    private int _health = 500;

    private void OnEnable()
    {
        EnemyObjectController.OnDestroyed += IncreaseScore;
        EnemyObjectController.OnCrashed += DamageHealth;

        HeartGameAPIClient.OnQuestionFetched += LogData;
    }

    private void OnDisable()
    {
        EnemyObjectController.OnDestroyed -= IncreaseScore;
        EnemyObjectController.OnCrashed -= DamageHealth;

        HeartGameAPIClient.OnQuestionFetched -= LogData;
    }

    private void IncreaseScore(int score)
    {
        _score += score;
    }

    private void DamageHealth(int amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            // error
            HeartGameAPIClient.Instance.FetchQuestion();
        }
    }

    void LogData(HeartGameAPIQuestion question)
    {
        Debug.Log(question);
    }
}
