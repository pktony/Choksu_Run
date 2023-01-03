using UnityEngine;
using TMPro;

public class Currency_UI : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    private int tempGold;
    private int gold;

    [SerializeField]
    private int increaseFactor = 100;
    private int goldIncreaseSpeed = 3;

    private bool isUpdate = false;

    private void Awake()
    {
        Transform goldParent = transform.GetChild(0);
        goldText = goldParent.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Inst.onGoldChange += UpdateGold;
    }

    private void Update()
    {
        if(isUpdate)
        {
            tempGold += goldIncreaseSpeed;
            goldText.text = tempGold.ToString("000000");
            if (tempGold >= gold)
            {
                goldText.text = gold.ToString();
                isUpdate = false;
            }
        }
    }

    private void UpdateGold(int currentGold, int gold)
    {
        isUpdate = true;
        tempGold = currentGold;
        this.gold = gold;

        goldIncreaseSpeed = gold / (tempGold + increaseFactor);
    }
}
