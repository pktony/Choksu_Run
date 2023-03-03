using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class CharacterManager : MonoBehaviour
{
    private Characters currentCharacter = Characters.kenny_Blue;

    private Characters currentCharacter_Customize;

    private GameObject currentShowingModel;

    public Characters CurrentCharacter => currentCharacter;


    public void SetCurrentCharacter_Customize(Characters selectedCharacter)
    {
        if (currentCharacter_Customize == selectedCharacter) return;
        currentCharacter_Customize = selectedCharacter;

        //임시
        // 미리 로드 ?
        if (!ReferenceEquals(currentShowingModel, null))
            Destroy(currentShowingModel);
        currentShowingModel = Instantiate(
            GameManager.Inst.resource.CharacterData[(int)currentCharacter_Customize].model);

        currentShowingModel.transform.position = Vector2.zero;
    }
}
