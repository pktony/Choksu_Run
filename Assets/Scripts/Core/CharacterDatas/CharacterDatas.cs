using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Object/Characters", order = 0)]
public class CharacterDatas : ScriptableObject
{
    public string characterName;
    public float price;
    public Sprite icon;

    public Sprite characterSprite;
    public RuntimeAnimatorController animatorController;
}

