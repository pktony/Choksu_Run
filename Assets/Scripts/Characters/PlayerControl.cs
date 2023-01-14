using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Rigidbody2D rigid;
    private Transform model;
    private Animator anim;

    private bool isGrounded = true;
    private int jumpCounter = 0;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float dieForce = 5.0f;
    [SerializeField]
    private float dieTorque = 5.0f;

    [Range(0f, 1f)]
    [SerializeField]
    private float shrinkMagnitude;

    [SerializeField]
    private bool isDoubleJump = false;
    [SerializeField]
    private int maxJumpNumber = 3;

    private void Awake()
    {
        inputActions = new();
        rigid = GetComponent<Rigidbody2D>();
        model = transform.GetChild(0);
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.Duck.performed += OnDuck;
        inputActions.Player.Duck.canceled += OnDuck;
    }

    private void OnDisable()
    {
        DisableAllControl();
    }

    private void OnDuck(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 newScale = Vector2.one - Vector2.up * shrinkMagnitude;
            transform.localScale = newScale;
        }
        else if(context.canceled)
        {
            transform.localScale = Vector2.one;
        }
    }

    private void OnJump(InputAction.CallbackContext _)
    {
        if (!isDoubleJump)
        {
            if (isGrounded)
            {
                //Debug.Log("Jump");
                isGrounded = false;
                rigid.velocity = jumpForce * Vector3.up;
                //rigid.AddForce(jumpForce * Vector3.up, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (jumpCounter < maxJumpNumber)
            {
                rigid.velocity = jumpForce * Vector3.up;
                //rigid.AddForce(jumpForce * Vector3.up, ForceMode2D.Impulse);
                jumpCounter++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Vector2 forceDir = collision.GetContact(0).normal;
            ExertForce(forceDir);
            GameManager.Inst.IsGameOver = true;
            return;
        }

        if(collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCounter = 0;
        }

    }

    private void DisableAllControl()
    {
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Duck.performed -= OnDuck;
        inputActions.Player.Duck.canceled -= OnDuck;
        inputActions.Player.Disable();
    }

    public void ExertForce(Vector2 direction)
    {
        DisableAllControl();
        rigid.freezeRotation = false;
        rigid.AddForce((direction + Vector2.up) * dieForce, ForceMode2D.Impulse);
        rigid.AddTorque(dieTorque, ForceMode2D.Impulse);

        anim.SetTrigger("onDie");
    }
}
