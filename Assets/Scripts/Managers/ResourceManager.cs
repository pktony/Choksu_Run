using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    //임시
    // 어드레서블에서 로딩 후 입력할 수 있도록 변경
    [SerializeField]
    private CharacterDatas[] characterData;


    public CharacterDatas[] CharacterData => characterData;
}
