using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance = null;
    public static T Inst
    {
        get
        {
            if (instance == null)
            {
                T obj = FindObjectOfType<T>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    GameObject gameObject = new();
                    gameObject.name = $"{typeof(T).Name}";
                    instance = gameObject.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)       
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);   // 나를 삭제.
            }
        }
    }

    /// <summary>
    /// 씬이 로딩될 때 실행될 델리게이트에 등록한 함수
    /// </summary>
    /// <param name="scene">해당 씬 데이터</param>
    /// <param name="mode">씬 추가 모드</param>
    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    }
}