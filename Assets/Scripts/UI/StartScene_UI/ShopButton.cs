using UnityEngine;
using UnityEngine.UI;


public class ShopButton : MonoBehaviour
{
    [SerializeField] private Button shopButton = default;

    private void Awake()
    {
        shopButton.onClick.AddListener(() =>
        {
            CommonPopup.Inst.OpenCommonPopup("Notice", "Coin Shop Comming Soon");
        });
    }
}
