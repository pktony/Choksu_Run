using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Platforms //장애물 객체의 행동
{
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
        transform.Translate(speed * Time.deltaTime * Vector2.left);

        if (transform.position.x < -18.0f)
        {
            ReturnPool();
        }
    }
}
