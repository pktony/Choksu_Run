using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 추가 수정이 필요할 수 있음
/// </summary>
public class PlayerModelSwitcher : MonoBehaviour
{
    private CharacterDatas data;

    private void Start()
    {
        // TODO
        // 나중에 스타트 씬에서 가져오기
        Define.Characters currentCharacter = GameManager.Inst.CharManager.CurrentCharacter;
        data = GameManager.Inst.resource.CharacterData[(int)currentCharacter];
        InitializeCharacter();
    }

    private void InitializeCharacter()
    {
        GameObject obj = Instantiate(data.model, this.transform);
        obj.transform.SetAsFirstSibling();
    }
}
