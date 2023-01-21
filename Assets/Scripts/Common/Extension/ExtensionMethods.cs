using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void DoesNothingMethod(this Transform _transform)
    {
        _transform.position = Vector3.zero;
    }
}
