using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Platforms
{
    [SerializeField]
    protected Define.CurrencyType type;

    public Define.CurrencyType CurrencyType => type;

    protected override void MovePlatform()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);

        if (transform.position.x < -18.0f)
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
        poolManager.ReturnPooledObject(this.gameObject, type);

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            TouchAction();
        }
    }
}
