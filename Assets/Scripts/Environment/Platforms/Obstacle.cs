using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Platforms<Define.ObstacleType> //장애물 객체의 행동
{
    protected override void EnablingAction()
    {
        
    }

    protected override void ReturnPool()
    {
        poolManager.ReturnPooledObject(this.gameObject, type);
    }

    protected override bool TouchAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void MovePlatform()
    {
        speed = gameManager.speed;
        rigid.MovePosition(rigid.position + speed * Time.fixedDeltaTime * Vector2.left);

        if (transform.position.x < leftEnd)
        {
            ReturnPool();
        }
    }
}
