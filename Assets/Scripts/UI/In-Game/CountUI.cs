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



    private void Start()
    {
        InitRoutine();
        GameManager.Inst.sound.PlayBGM(BGM.GameScene, (int)PlayType.Direct);
    }

    private void OnDisable()
    {
        Debug.Log(GameManager.Inst.Status.ToString());
    }

    public void InitRoutine()
    {
        if(!GameManager.Inst.IsGameOver)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }
            routine = StartCoroutine(CountDown(time));
        }
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

        //TODO : ĳ���� Animator , Run���� Ʈ���� �۵��ϱ� (��� ĳ���� Animator �۾��� Run Ʈ���� �߰�)
        if (anim == null)
        {
            anim = player.GetChild(0).GetComponent<Animator>();
        }
        anim.SetTrigger("Run");

        GameManager.Inst.SetStatus(GameManager.GameStatus.Run);

        countText.text = string.Empty;
        routine = null;
        gameObject.SetActive(false);
    }
}
