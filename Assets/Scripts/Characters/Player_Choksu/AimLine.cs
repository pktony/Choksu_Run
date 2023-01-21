using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimLine : MonoBehaviour
{
    private LineRenderer aimLine;

    private void Awake()
    {
        aimLine = GetComponent<LineRenderer>();
        aimLine.useWorldSpace = false;
    }

    public void DrawLine(Vector3 position)
    {
        aimLine.enabled = true;
        aimLine.SetPosition(0, Vector3.zero);
        aimLine.SetPosition(1, position);
    }

    public void DisableAimLine()
    {
        aimLine.enabled = false;
    }
}