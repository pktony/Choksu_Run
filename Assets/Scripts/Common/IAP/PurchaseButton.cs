using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PurchaseButton : MonoBehaviour
{
    private Button purchaseBtn = null;

    public string targetProductId;

    private void Start()
    {
        purchaseBtn = GetComponent<Button>();
        purchaseBtn.onClick.AddListener(HandleClick);
    }


    public void HandleClick()
    {
        if(targetProductId == IAPManager.productIDNonConsumable || targetProductId == IAPManager.productIDSubscription)
        {
           if(IAPManager.Inst.HadPurchased(targetProductId))
            {
                Debug.Log("�̹� ������ ��ǰ �Դϴ�");
                return;
            }
        }

        IAPManager.Inst.Purchase(targetProductId);
    }
}
