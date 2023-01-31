using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Group : Platforms<Define.CurrencyType>
{
    private float groupSize_X;

    //시작할 때 자식들 켜줘야 함

    protected override void CalculateGroupSize()
    {
        groupSize_X = float.MinValue;
        for(int i = 0; i < transform.childCount;i++)
        {
            if(transform.GetChild(i).position.x > groupSize_X)
            {
                groupSize_X = transform.GetChild(i).localPosition.x;
            }
        }
    }

    protected override void MovePlatform()
    {
        transform.Translate(speed * Time.fixedDeltaTime * Vector2.left);

        if (transform.position.x < leftEnd - groupSize_X)
        {//전체 사이즈를 계산하는 방법을 찾아야 됨
            ReturnPool();
        }
    }

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
}
