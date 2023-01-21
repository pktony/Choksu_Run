using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Transform model;
    private Animator anim;
    protected Rigidbody2D rigid;

    protected bool isGrounded = true;
    private int jumpCounter = 0;

    [Header("Jump")]
    [SerializeField]
    private bool isDoubleJump = false;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int maxJumpNumber = 3;

    [Header("Die")]
    [SerializeField]
    private float dieForce = 5.0f;
    [SerializeField]
    private float dieTorque = 5.0f;

    [Header("Duck")]
    [Range(0f, 1f)]
    [SerializeField]
    private float shrinkMagnitude;

    public virtual bool IsGrounded
    {
        get => isGrounded;
        protected set
        {
            isGrounded = value;
        }
    }

    protected virtual void Awake()
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
        inputActions.Player.Skill.performed += OnSkill;
        inputActions.Player.Skill.canceled += OnSkill;
    }

    private void OnDisable()
    {
        DisableAllControl();
    }

    private void OnDuck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 newScale = Vector2.one - Vector2.up * shrinkMagnitude;
            transform.localScale = newScale;
        }
        else if (context.canceled)
        {
            transform.localScale = Vector2.one;
        }
    }

    private void OnJump(InputAction.CallbackContext _)
    {
        Jump();
    }

    protected virtual void Jump()
    {
        if (!isDoubleJump)
        {
            if (IsGrounded)
            {
                IsGrounded = false;
                rigid.velocity = rigid.velocity.x * Vector3.right + jumpForce * Vector3.up;
            }
        }
        else
        {
            if (jumpCounter < maxJumpNumber)
            {
                rigid.velocity = rigid.velocity.x * Vector3.right + jumpForce * Vector3.up;
                jumpCounter++;
            }
        }
    }

    protected virtual void OnSkill(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
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

        if (collision.collider.CompareTag("Ground"))
        {
            IsGrounded = true;
            jumpCounter = 0;
        }
    }

    private void DisableAllControl()
    {
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Duck.performed -= OnDuck;
        inputActions.Player.Duck.canceled -= OnDuck;
        inputActions.Player.Skill.performed -= OnSkill;
        inputActions.Player.Skill.canceled -= OnSkill;
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
