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
    [SerializeField]
    private ControlMode mode = ControlMode.normal;

    private AimLine aimLine;
    private ChokLine chokLine;

    private Vector2 chokDirection;
    private WaitForSeconds forceWaitSeconds;

    private Vector2 defaultPosition;
    private bool isAtDefaultPosition = false;
    private float currentSpeed;

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
                //currentSpeed = transform.position.x <= defaultPosition.x ?
                //    returningSpeed : -returningSpeed;

                //StartCoroutine(ReturnOringinalPosition());
            }
        }
    }

    #region UNITY EVENT 함수 ###################################################
    protected override void Awake()
    {
        base.Awake();
        aimLine = transform.GetChild(1).GetComponent<AimLine>();
        chokLine = transform.GetChild(2).GetComponent<ChokLine>();
        forceWaitSeconds = new WaitForSeconds(forceInterval);

        defaultPosition = transform.position;

        chokLine.onChokAttached += RotateCharacter;
    }

    protected override void OnSkill(InputAction.CallbackContext context)
    {
        if (mode == ControlMode.chok)
            return;

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

#if UNITY_EDITOR
            Debug.DrawRay(transform.position, newDirection, Color.red);
            Debug.DrawRay(transform.position, forceDir);
#endif
            rigid.velocity = forceDir * chokSpeed;
            yield return null;
        }
    }

    private void DetachChok()
    {
        chokLine.DisableChok();
        rigid.gravityScale = gravityScale;
        mode = ControlMode.normal;
    }

    private void UseSkill()
    {
        Time.timeScale = 0.3f;
        mode = ControlMode.aim;
        StartCoroutine(Aim());
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

    //private IEnumerator ReturnOringinalPosition()
    //{

    //    while (!isAtDefaultPosition)
    //    {
    //        rigid.velocity = rigid.velocity.y * Vector2.up + currentSpeed * Vector2.right;

    //        if (transform.position.x - defaultPosition.x < 0.2f)
    //        {
    //            isAtDefaultPosition = true;
    //            rigid.position = defaultPosition;
    //        }

    //        yield return null;
    //    }
    //}
    #endregion
}