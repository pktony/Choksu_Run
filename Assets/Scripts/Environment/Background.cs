using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float speed;
    private float offset_x;

    private Material mat;

    private Coroutine routine = null;


    private void Start()
    {
        mat = GetComponent<Renderer>().material;

        if(routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(ScrollBackGround());
    }

    private IEnumerator ScrollBackGround()
    {
        yield return new WaitUntil(() => GameManager.Inst.Status.Equals(GameManager.GameStatus.Run));

        while(GameManager.Inst.Status.Equals(GameManager.GameStatus.Run))
        {
            offset_x += (speed * Time.deltaTime);

            Vector2 offset = new Vector2(offset_x, 0);
            mat.SetTextureOffset("_MainTex", offset);

            yield return null;
        }

        yield return null;
    }
}
