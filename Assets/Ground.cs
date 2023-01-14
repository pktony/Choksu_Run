using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Platforms
{

    private void Update()
    {
       // MovePlatform();
    }






    protected override void MovePlatform()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);
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
