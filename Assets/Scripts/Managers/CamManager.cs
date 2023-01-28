using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    private float camBound_X;
    private const float END_CORRECTION_VALUE = 1f;

    private void Awake()
    {
        Vector3 rightEnd = new(Screen.width, 0f, 0f);
        camBound_X = Camera.main.ScreenToWorldPoint(rightEnd).x;
    }


    public float GetLeftEnd() => -camBound_X - END_CORRECTION_VALUE;
}
