using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 체력 개념이 있을 때 사용
/// 싱글톤 매니저로 관리
/// </summary>
public class PlayerStats : MonoBehaviour
{
    private PlayerControl playerControl;

    [SerializeField]
    private int maxHP = 3;

    private int healthPoint;
    
    public int HP
    {
        get => healthPoint;
        set
        {
            healthPoint = Mathf.Clamp(value, 0, maxHP);
            if(healthPoint < value)
            {// HP 감소

            }
            else if(healthPoint > value)
            {// HP 증가

            }

            if(healthPoint == 0)
            {
                Die();
            }
        }
    }

    public int MaxHP => maxHP;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
        healthPoint = maxHP;
    }

    private void Die()
    {
    }
}
