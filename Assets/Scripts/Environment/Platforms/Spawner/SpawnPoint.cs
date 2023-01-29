using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        transform.position = GameManager.Inst.CameraManager.GetRightEnd() * Vector2.right;
    }
}
