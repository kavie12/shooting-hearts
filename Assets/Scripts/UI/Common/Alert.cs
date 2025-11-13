using System.Collections;
using TMPro;
using UnityEngine;

public class AlertPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _alert;

    private void OnEnable()
    {
        EventBus.Subscribe<ShowAlertEvent>(ShowAlert);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<ShowAlertEvent>(ShowAlert);
    }

    public void ShowAlert(ShowAlertEvent e)
    {
        StartCoroutine(SetAlert(e.Message, e.Duration));
    }

    private IEnumerator SetAlert(string message, float duration)
    {
        _alert.text = message;
        _alert.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        _alert.gameObject.SetActive(false);
    }
}
