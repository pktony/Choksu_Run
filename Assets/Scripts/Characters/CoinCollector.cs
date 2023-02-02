using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coins"))
        {
            if(collision.TryGetComponent(out ICoin coin))
            {
                GameManager.Inst.Score.InGameCoin += coin.Worth;
                UIs.UI_PopupText popupText = GameManager.Inst.PoolManager.GetPooledUIs(Define.UIPoolType.popupText);
                popupText.ShowText(coin.Worth.ToString(), 0.5f,transform.position, Color.black, 10f);
                popupText.gameObject.SetActive(true);
            }
        }
    }
}
