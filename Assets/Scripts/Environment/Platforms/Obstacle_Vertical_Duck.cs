using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Obstacle_Vertical_Duck : Platforms<ObstacleType>
{
    protected override void EnablingAction()
    {
        //throw new System.NotImplementedException();
    }

    protected override void MovePlatform()
    {
        speed = GameManager.Inst.GetSpeed();

        rigid.MovePosition(rigid.position + speed * Time.fixedDeltaTime * Vector2.left);

        // 나중에 플레이어 쑤구리 가능한 높이 설정
        if(rigid.position.y > 3.0f)
            rigid.MovePosition(rigid.position + speed * 3f * Time.fixedDeltaTime * Vector2.down);

        if (transform.position.y < leftEnd)
        {
            ReturnPool();
        }
    }

    protected override void ReturnPool()
    {
        throw new System.NotImplementedException();
    }

    protected override bool TouchAction()
    {
        throw new System.NotImplementedException();
    }
}
