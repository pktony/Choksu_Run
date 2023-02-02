using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Object/Characters", order = 0)]
public class CharacterDatas : ScriptableObject
{
    public string characterName;
    public Characters character;
    public int price;
    public Sprite icon;

    public GameObject model;
}

