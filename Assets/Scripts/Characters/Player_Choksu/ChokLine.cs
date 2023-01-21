using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class ChokLine : MonoBehaviour
{
    private LineRenderer chokLine;
    private Collider2D chokCollider;

    private Vector2 destination;
    private Vector2 direction;

    private bool isAttached = false;

    [SerializeField]
    private float chokSpeed = 3.0f;
    [SerializeField]
    private float maxChokTime = 1.0f;

    public System.Action<Vector2> onChokAttached;

    private void Awake()
    {
        chokLine = GetComponent<LineRenderer>();
        chokCollider = GetComponent<Collider2D>();
        chokCollider.isTrigger = true;
        chokCollider.enabled = false;
    }

    /// <summary>
    /// 촉수를 발사하는 함수
    /// </summary>
    /// <param name="direction"> 발사 방향 </param>
    public void ShootChok(Vector2 direction)
    {
        destination = transform.position;
        chokLine.enabled = true;
        chokCollider.enabled = true;
        this.direction = direction;

        StartCoroutine(ExtendChok());
    }

    /// <summary>
    /// 촉수 비활성화
    /// </summary>
    public void DisableChok()
    {
        chokLine.enabled = false;
        chokCollider.enabled = false;
        isAttached = false;
    }

    private IEnumerator ExtendChok()
    {
        float timer = 0f;
        while (timer < maxChokTime && !isAttached)
        {
            chokLine.SetPosition(0, transform.position);
            destination += chokSpeed * Time.deltaTime * direction;
            chokLine.SetPosition(1, destination);
            chokCollider.offset = destination - (Vector2)transform.position;
            timer += Time.deltaTime;
            yield return null;
        }
        if(timer >= maxChokTime)
        {
            DisableChok();
        }
    }

    private IEnumerator UpdateLinePosition()
    {
        while (isAttached)
        {
            chokLine.SetPosition(0, transform.position);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        chokCollider.enabled = false;
        isAttached = true;
        onChokAttached?.Invoke(destination);
        StartCoroutine(UpdateLinePosition());

        Debug.Log($"Chok attached to : {collision.name}");
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isAttached)
        {
            UnityEditor.Handles.DrawWireDisc(destination, Vector3.forward,
                Vector2.Distance(destination, transform.position));
        }
    }
#endif
}