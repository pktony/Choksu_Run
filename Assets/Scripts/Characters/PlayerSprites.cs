using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    [SerializeField]
    private Sprite baseSprite;
    [SerializeField]
    private RuntimeAnimatorController animatorController;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void InitializeCharacter()
    {
        spriteRenderer.sprite = baseSprite;
        anim.runtimeAnimatorController = animatorController;
    }
}
