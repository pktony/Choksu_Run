using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine.Purchasing;



public class IAPManager : Singleton<IAPManager>, IStoreListener, IBootingComponent
{

    private string environment = "production"; // Unity Services Initialize

    public const string productIDConsumable = "consumable"; //�Ҹ�, �ΰ��� ��ȭ , ������ , ũ����Ż
    public const string productIDNonConsumable = "nonconsumable"; //��Ҹ�, ��������, ĳ���� ��Ų
    public const string productIDSubscription = "subscription";//������, vip

    //Android ��ǰ ID , ���� �÷��� �ֿܼ��� �ξ۰��� �������� ID
    private string _Android_ConsumableId = "consumable";
    private string _Android_NonconsumableId = "nonconsumable";
    private string _Android_SubscriptionId = "subscription";

    //iOS ��ǰ ID
    private string _IOS_ConsumableId = "com.Mocharm.app.consumable.ios";
    private string _IOS_NonconsumableId = "com.Mocharm.app.nonconsumable.ios";
    private string _IOS_SubscriptionId = "com.Mocharm.app.subscription.ios";

    private IStoreController storeController; //���� ������ �����ϴ� �Լ��� ����
    private IExtensionProvider storeExtensionProvider;// ���� �÷����� ���� Ȯ�� ó���� ����

    public bool IsInitialized => storeController != null && storeExtensionProvider != null;

    #region IBootingComponent
    private bool isReady = false;
    public bool IsReady => isReady;
    #endregion

    private void Start()
    {
        InitUnityServices();
        InitUnityIAP();
        StartCoroutine("HadSubscription");

        isReady = true;
    }

    private void InitUnityServices()
    {
        try
        {
            var options = new InitializationOptions()
                .SetEnvironmentName(environment);

            UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
            // An error occurred during services initialization.
            Debug.LogError(exception);
        }
    }

    #region IStoreListener
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        Debug.Log("����Ƽ IAP �ʱ�ȭ ����");
        storeController = controller;
        storeExtensionProvider = extension;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"����Ƽ IAP �ʱ�ȭ ���� {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning($"���� ���� - {product.definition.id}, {reason}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.Log($"���� ���� - ID : {purchaseEvent.purchasedProduct.definition.id}");

        if (purchaseEvent.purchasedProduct.definition.id == productIDConsumable)
        {
            Debug.Log("�Ҹ�ǰ ���� �Ϸ�");
        }
        else if (purchaseEvent.purchasedProduct.definition.id == productIDNonConsumable)
        {
            Debug.Log("�� �Ҹ�ǰ ���� �Ϸ�");
        }
        else if (purchaseEvent.purchasedProduct.definition.id == productIDSubscription)
        {
            Debug.Log("���� ���� ���� �Ϸ�");
        }

        return PurchaseProcessingResult.Complete;
    }
    #endregion

    private void InitUnityIAP()
    {
        if (IsInitialized) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(productIDConsumable, ProductType.Consumable, new IDs()
        {
            {_Android_ConsumableId, GooglePlay.Name },
                        {_IOS_ConsumableId, AppleAppStore.Name}
        }
        );

        builder.AddProduct(productIDNonConsumable, ProductType.NonConsumable, new IDs()
        {
            {_Android_NonconsumableId, GooglePlay.Name },
                        {_IOS_NonconsumableId, AppleAppStore.Name}
        }
       );

        builder.AddProduct(productIDSubscription, ProductType.Subscription, new IDs()
        {
            {_Android_SubscriptionId, GooglePlay.Name },
                        {_IOS_SubscriptionId, AppleAppStore.Name}
        }
       );
        UnityPurchasing.Initialize(this, builder);
    }

    public void Purchase(string productId)
    {
        if (!IsInitialized) return;

        var product = storeController.products.WithID(productId);

        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"���� �õ� - {product.definition.id}");
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log($"���� �õ� �Ұ� - {productId}");
        }
    }

    //AppStore 심사 에서는 필수적으로 환불기능을 넣어줘야 통과됨
    public void RestorePurchase()
    {
        if (!IsInitialized) return;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("���� ���� �õ�");

            var appleExtension = storeExtensionProvider.GetExtension<IAppleExtensions>();
            appleExtension.RestoreTransactions(
                result => Debug.Log($"���� ���� �õ� ��� - {result}"));
        }
    }

    public bool HadPurchased(string productId)
    {
        if (!IsInitialized) return false;

        var product = storeController.products.WithID(productId);

        if(product != null)
        {
            return product.hasReceipt;
        }

        return false;
    }

    private IEnumerator HadSubscription()
    {
        yield return new WaitForSeconds(3f);
        if (HadPurchased(productIDSubscription))
        {
            Debug.Log("�̹� ���� ��");
        }
    }
}
