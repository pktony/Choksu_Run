using System.Collections;
using UnityEngine;

using TMPro;

public class NotificationUI : MonoBehaviour
{
    private UIs.UI_Slide notificationPanel;

    [SerializeField] private TextMeshProUGUI notificationText = default;
    [SerializeField] private string textText;
    [SerializeField] private float showTime;

    private void Awake()
    {
        notificationPanel = GetComponent<UIs.UI_Slide>();    
    }

    void Update()
    {
        if(UnityEngine.InputSystem.Keyboard.current.kKey.wasPressedThisFrame)
        {
            ShowNotification(textText);
        }
    }

    public void ShowNotification(string notificationText)
    {
        this.notificationText.text = notificationText;

        notificationPanel.Slide(() => StartCoroutine(HideNotificationAfter(showTime)));
    }

    private IEnumerator HideNotificationAfter(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);

        notificationPanel.Slide();
    }
}
