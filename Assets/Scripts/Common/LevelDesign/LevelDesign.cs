using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    public ObstacleInfos[] obstacleInfos;

    [Header("Level Design")]
    [SerializeField]
    private string levels;

    public float GetTime(Define.ObstacleType type)
    {
        return obstacleInfos[(int)type].obstacleTime;
    }
}
