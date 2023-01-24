using System.Collections;
using System.Collections.Generic;
using Define;
using UnityEngine;

public class Coin : Platforms, ICoin
{
    [SerializeField]
    protected Define.CurrencyType type;
    [SerializeField]
    private int worth = 1;

    public CurrencyType Type => type;

    public int Worth => worth;

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
        poolManager.ReturnPooledObject(this.gameObject, type);
    }

    protected override bool TouchAction()
    {
        ReturnPool();

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ItemCollector"))
        {
            TouchAction();
        }
    }
}
