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
        poolManager.ReturnObstacle(this);
    }

    protected override bool TouchAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void MovePlatform()
    {
        speed = gameManager.GetSpeed();
        rigid.MovePosition(rigid.position + speed * Time.fixedDeltaTime * Vector2.left);

        if (transform.position.x < leftEnd - size_X)
        {
            ReturnPool();
        }
    }
}
