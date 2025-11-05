using System.Collections;
using TMPro;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public static Alert instance;

    [SerializeField] private TextMeshProUGUI _alert;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowAlert(string message, float duration)
    {
        StartCoroutine(SetAlert(message, duration));
    }

    public void ShowAlert(string message)
    {
        StartCoroutine(SetAlert(message, 2f));
    }

    private IEnumerator SetAlert(string message, float duration)
    {
        _alert.text = message;
        _alert.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        _alert.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
