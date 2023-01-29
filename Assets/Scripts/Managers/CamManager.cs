using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    private float camBound_X;
    private float camBount_Y;
    private const float END_CORRECTION_VALUE = 1f;

    private void Awake()
    {
        Vector3 rightEnd = new(Screen.width, 0f, 0f);
        camBound_X = Camera.main.ScreenToWorldPoint(rightEnd).x;

        Vector3 topEnd = new(Screen.height, 0f, 0f);
        camBount_Y = Camera.main.ScreenToWorldPoint(topEnd).y;
    }


    public float GetLeftEnd() => -camBound_X - END_CORRECTION_VALUE;
    public float GetRightEnd() => -GetLeftEnd();
    public float GetTopEnd() => camBount_Y;
    public float GetBotEnd() => -camBount_Y;
}
