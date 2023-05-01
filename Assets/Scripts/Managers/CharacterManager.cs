using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class CharacterManager : MonoBehaviour
{
    private GameObject currentShowingModel;

    public Characters CurrentCharacter { get; private set; }
    public Characters CurrentCharacter_Customize { get; private set; }


    public void SetCurrentCharacter_Customize(Characters selectedCharacter)
    {
        if (CurrentCharacter_Customize == selectedCharacter) return;
        CurrentCharacter_Customize = selectedCharacter;

        //임시
        // 미리 로드 ?
        if (!ReferenceEquals(currentShowingModel, null))
            Destroy(currentShowingModel);
        currentShowingModel = Instantiate(
            GameManager.Inst.resource.CharacterData[(int)CurrentCharacter_Customize].model);

        currentShowingModel.transform.position = Vector2.zero;
    }

    public void GetCurrentCharacter()
    {
        if (!ReferenceEquals(currentShowingModel, null))
            Destroy(currentShowingModel);

        currentShowingModel = Instantiate(
            GameManager.Inst.resource.CharacterData[(int)CurrentCharacter].model);

        currentShowingModel.transform.position = Vector2.zero;

        currentShowingModel.GetComponent<Animator>().SetTrigger("Run");
    }
}
