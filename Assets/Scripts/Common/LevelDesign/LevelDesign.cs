using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    public ObstacleInfos[] obstacleInfos;

    [Header("Level Design")]
    [Tooltip("0 :  1 :  c : coin")]
    [SerializeField]
    private string levelString;

    /// <summary>
    /// 장애물 간격 시간
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float GetTime(Define.ObstacleType type)
    {
        return obstacleInfos[(int)type].obstacleTime;
    }

    /// <summary>
    /// 장애물 종류 개수
    /// </summary>
    public int LevelCount => obstacleInfos.Length;


    public List<char> GetLevels(out List<char> obstacleChars)
    {
        List<char> levels = new List<char>(levelString.Length);
        obstacleChars = new List<char>(levelString.Length);

        for (int i = 0; i < levelString.Length; i++)
        {
            if (levelString[i] == 'c')
            {
                levels.Add(levelString[i]);
            }
            else
            {
                levels.Add(levelString[i]);
            }

            if (!obstacleChars.Contains(levelString[i]))
                obstacleChars.Add(levelString[i]);
        }

        return levels;
    }

    /// int 버전

    //public List<int> GetLevels(out List<char> obstacleChars)
    //{
    //    List<int> levels = new List<int>(levelString.Length);
    //    obstacleChars = new List<char>(levelString.Length);

    //    for (int i = 0; i < levelString.Length; i++)
    //    {
    //        if (levelString[i] == 'c')
    //        {
    //            levels.Add(levelString[i] - 'a');
    //        }
    //        else
    //        {
    //            levels.Add(levelString[i] - '0');
    //        }

    //        if (!obstacleChars.Contains(levelString[i]))
    //            obstacleChars.Add(levelString[i]);
    //    }

    //    return levels;
    //}
}
