using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 작업중
/// 마우스 위치가 아닌 터치 위치를 가져와야함
/// </summary>
public class PlayerControl_Chok : PlayerControl
{
    private enum ControlMode { normal, aim, chok };
    [Header("Control Mode")]
    private ControlMode mode = ControlMode.normal;

    private AimLine aimLine;
    private ChokLine chokLine;

    private Vector2 chokDirection;
    private WaitForSeconds forceWaitSeconds;

    private Vector2 defaultPosition;
    private bool isAtDefaultPosition = false;
    private float currentSpeed;

    private IEnumerator returnHandler = null;
    private IEnumerator aimHandler = null;

    [Header("SKill")]
    [SerializeField]
    private float returningSpeed = 5f;
    [SerializeField]
    private float chokSpeed = 10f;
    [SerializeField]
    private float forceInterval = 0.5f;

    public override bool IsGrounded
    {
        get => isGrounded;
        protected set
        {
            isGrounded = value;
            if (isGrounded)
            {
                chokLine.DisableChok();
                mode = ControlMode.normal;
                rigid.gravityScale = gravityScale;
            }
        }
    }

    #region UNITY EVENT 함수 ###################################################
    protected override void Awake()
    {
        base.Awake();
        aimLine = GetComponentInChildren<AimLine>();
        chokLine = GetComponentInChildren<ChokLine>();
        forceWaitSeconds = new WaitForSeconds(forceInterval);

        defaultPosition = new Vector2(transform.position.x, 0f) ;

        chokLine.onChokAttached += RotateCharacter;
        chokLine.onAutoChokDisabled += SetModeNormal;
    }

    protected override void OnSkill(InputAction.CallbackContext context)
    {
        if (mode == ControlMode.chok)
            return;
        DetachChok();
        if (context.performed)
        {
            UseSkill();
        }
        else if (context.canceled)
        {
            ShootChok();
        }
    }
    #endregion
    
    protected override void Jump()
    {
        if (mode == ControlMode.chok)
        {
            DetachChok();
            return;
        }
        base.Jump();
    }

    protected override void GroundTouchAction()
    {
        base.GroundTouchAction();
        DetachChok();
    }

    #region PRIVATE 함수 #######################################################
    private void RotateCharacter(Vector2 attachedPoint)
    {
        rigid.gravityScale = 0f;
        StartCoroutine(SetVelocityPerpendicularTo(attachedPoint));
    }

    private IEnumerator SetVelocityPerpendicularTo(Vector2 attachedPoint)
    {
        while (mode == ControlMode.chok)
        {
            Vector2 newDirection = attachedPoint - (Vector2)transform.position;
            newDirection = newDirection.normalized;
            Vector2 forceDir = new Vector2(newDirection.y, -newDirection.x);
            rigid.velocity = forceDir * chokSpeed;
            yield return null;
        }
    }

    private void DetachChok()
    {
        chokLine.DisableChok();
        rigid.gravityScale = gravityScale;
        mode = ControlMode.normal;
        if (returnHandler != null) StopCoroutine(returnHandler);
        StartCoroutine(returnHandler = ReturnOringinalPosition());
    }

    private void UseSkill()
    {
        Time.timeScale = 0.3f;
        mode = ControlMode.aim;
        if (aimHandler != null) StopCoroutine(aimHandler);
        StartCoroutine(aimHandler = Aim());
    }

    private IEnumerator Aim()
    {
        while (mode == ControlMode.aim)
        {
            Vector2 mousePos = Touchscreen.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.x = Mathf.Max(worldPos.x, transform.position.x);
            chokDirection = worldPos - (Vector2)aimLine.transform.position;
            chokDirection = chokDirection.normalized;
            aimLine.DrawLine(chokDirection * 3f);
            yield return null;
        }
    }

    private void ShootChok()
    {
        Debug.Log("Chok !!!");
        Time.timeScale = 1.0f;
        aimLine.DisableAimLine();
        chokLine.ShootChok(chokDirection);
        mode = ControlMode.chok;
    }

    private void SetModeNormal()
    {
        mode = ControlMode.normal;
    }

    private IEnumerator ReturnOringinalPosition()
    {
        yield return new WaitUntil(() => isGrounded == true);
        currentSpeed = rigid.position.x <= defaultPosition.x ?
                    returningSpeed : -returningSpeed;

        Debug.Log($"Player Grounded : Current Speed {currentSpeed}");

        isAtDefaultPosition = false;

        while (!isAtDefaultPosition)
        {
            if ((currentSpeed < 0 && rigid.position.x < defaultPosition.x) || 
                    (currentSpeed > 0 && rigid.position.x > defaultPosition.x))
            {
                isAtDefaultPosition = true;
                rigid.position = new Vector2(defaultPosition.x, rigid.position.y);
                rigid.velocity = Vector2.zero;
                Debug.Log("Player at Default Posistion");
                break;
            }

            rigid.velocity = rigid.velocity.y * Vector2.up + currentSpeed * Vector2.right;

            yield return null;
        }
    }
    #endregion
}