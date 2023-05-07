using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Obstacle_Vertical : Platforms<ObstacleType>
{
    bool initialized = false;

    protected override void InitializeRigidbody()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.isKinematic = false;
    }

    protected override void MovePlatform()
    {
        speed = GameManager.Inst.GetSpeed();

        rigid.MovePosition(rigid.position + speed * Time.fixedDeltaTime * Vector2.left);

        if (transform.position.y < leftEnd)
        {
            ReturnPool();
        }
    }

    protected override void EnablingAction()
    {
        if (!initialized)
        {
            initialized = true;
            return;
        }

        rigid.gravityScale = GameManager.Inst.gravityScale;
        rigid.AddTorque(2f);
    }

    protected override void ReturnPool()
    {
        poolManager.ReturnObstacle(this);
    }

    protected override bool TouchAction()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            rigid.bodyType = RigidbodyType2D.Kinematic;
            rigid.Sleep();
        }
    }
}
