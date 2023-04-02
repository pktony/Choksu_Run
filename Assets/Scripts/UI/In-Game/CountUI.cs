using System;
using System.Collections;
using UnityEngine;

using Define;
using TMPro;


public class CountUI : MonoBehaviour
{
    [SerializeField] Transform player = null;
    [SerializeField] float time;
    [SerializeField] TextMeshProUGUI countText = null;

    private Animator anim = null;
    private Coroutine routine = null;


    private void OnEnable()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(CountDown(time));
    }

    private IEnumerator CountDown(float _time)
    {
        countText.text = $"{Convert.ToInt32(_time)}";

        while (_time > 0)
        {
            _time -= Time.deltaTime;
            countText.text = $"{Convert.ToInt32(_time)}";
            //TODO : Counting Sound Here!
            yield return null;
        }

        //TimeOver Then -- > 

        //TODO : 캐릭터 Animator , Run으로 트리거 작동하기 (모든 캐릭터 Animator 작업시 Run 트리거 추가)
        if (anim == null)
        {
            anim = player.GetChild(0).GetComponent<Animator>();
        }
        anim.SetTrigger("Run");

        GameManager.Inst.sound.PlayBGM(BGM.GameScene, (int)PlayType.Direct);
        GameManager.Inst.Status = GameManager.GameStatus.Run;

        countText.text = string.Empty;
        routine = null;
        gameObject.SetActive(false);
    }
}
