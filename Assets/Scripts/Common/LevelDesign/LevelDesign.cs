using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    public ObstacleInfos[] obstacleInfos;

    [Header("Level Design")]
    [SerializeField]
    private string levelString;

    public float GetTime(Define.ObstacleType type)
    {
        return obstacleInfos[(int)type].obstacleTime;
    }

    public int LevelCount => obstacleInfos.Length;

    public List<int> GetLevels()
    {
        List<int> levels = new List<int>(levelString.Length);

        for(int i = 0; i < levelString.Length; i++)
        {
            levels.Add(levelString[i] - '0');
        }

        return levels;
    }
}
