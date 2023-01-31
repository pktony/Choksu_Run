using System.Collections;
using System.Collections.Generic;
using Define;
using UnityEngine;

public class Coin : Platforms<CurrencyType>, ICoin
{
    [Header("Coin 정보")]
    [SerializeField]
    private int worth = 1;

    public CurrencyType Type => type;

    public int Worth => worth;

    protected override void MovePlatform()
    {
        rigid.MovePosition(rigid.position + speed * Time.fixedDeltaTime * Vector2.left);

        if (transform.position.x < leftEnd)
        {
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
        if (isGroupedChild)
            gameObject.SetActive(false);
        else
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
