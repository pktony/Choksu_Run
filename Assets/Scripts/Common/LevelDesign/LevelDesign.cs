using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

using Define;

public class LevelDesign : MonoBehaviour
{
    [Header("Level Design")]
    [Tooltip("무한반복")]
    public bool isObstacleTest = false;
    [Tooltip("Number : Obstacle , Alphabet : coin")]
    [SerializeField]
    private string levelString;

    [SerializeField, Range(0f, 1f)] private float obstacleCurrencyRatio = 0.8f;

    public ObstacleInfos[] obstacleInfos;
    public CurrencyInfos[] currencyInfos;

    private void GenerateRandomLevels(int levelVariationCount)
    {
        StringBuilder levels = new();

        int levelCount = 0;
        while (levelCount < levelVariationCount)
        {
            float levelType = Random.Range(0f, 1f);

            if (levelType < obstacleCurrencyRatio)
            {
                var randValue = Random.Range(0, (int)ObstacleType.Count);
                levels.Append(randValue.ToString());
            }
            else
            {
                var randValue = Random.Range(0, (int)CurrencyType.Count);
                char coinCharacter = (char)(randValue + 'a');
                Debug.Log(coinCharacter);
                levels.Append(coinCharacter.ToString());
            }
            levelCount++;
        }

        levelString = levels.ToString();
    }

    /// <summary>
    /// 장애물 간격 시간
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float GetTime(ObstacleType type)
    {
        return obstacleInfos[(int)type].obstacleTime;
    }

    public float GetTime(CurrencyType type)
    {
        return currencyInfos[(int)type].coinTime;
    }

    /// <summary>
    /// 장애물 종류 개수
    /// </summary>
    public int LevelCount => obstacleInfos.Length;


    public List<char> GetLevels(out List<char> obstacleChars)
    {
        GenerateRandomLevels(30);

        List<char> levels = new List<char>(levelString.Length);
        obstacleChars = new List<char>(levelString.Length);

        for (int i = 0; i < levelString.Length; i++)
        {
            levels.Add(levelString[i]);

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
