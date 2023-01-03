using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    UIs.UI_Slide notificationPanel;

    private void Awake()
    {
        notificationPanel = GetComponent<UIs.UI_Slide>();    
    }

    void Update()
    {
        if(UnityEngine.InputSystem.Keyboard.current.kKey.wasPressedThisFrame)
        {

        }
            
    }
}
