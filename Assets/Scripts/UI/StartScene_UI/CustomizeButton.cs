using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomizeButton : MonoBehaviour
{
    [SerializeField] private Button customizeButton = default;


    private void Awake()
    {
        customizeButton.onClick.AddListener(() =>
        {
            GameManager.Inst.sound.PlaySFX(Define.SFX.Click);
            CommonPopup.Inst.OpenCommonPopup("Notice", "Customization Comming Soon...");  
        });
    }
}
