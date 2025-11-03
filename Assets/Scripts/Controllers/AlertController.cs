using System.Collections;
using TMPro;
using UnityEngine;

public class AlertController : MonoBehaviour
{
    public static AlertController instance;

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

    public void ShowAlert(string message)
    {
        StartCoroutine(SetAlert(message));
    }

    private IEnumerator SetAlert(string message)
    {
        _alert.text = message;
        _alert.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _alert.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
