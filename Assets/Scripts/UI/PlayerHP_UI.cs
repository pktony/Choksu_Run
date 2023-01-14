using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP_UI : MonoBehaviour
{
    private PlayerStats stats;

    [SerializeField]
    private Image hpImage;

    private void Awake()
    {
        // 임시 => 추후 매니저에서 가져올 예정
        stats = FindObjectOfType<PlayerStats>();

        for (int i = 0; i < stats.MaxHP; i++)
        {
            Instantiate(hpImage, this.transform);
        }
    }

}
