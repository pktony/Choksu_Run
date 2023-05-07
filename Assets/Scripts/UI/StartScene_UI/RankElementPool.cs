using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RankElementPool : MonoBehaviour
{
    [SerializeField] private RankElement rankElementObj;

    private Queue<RankElement> elementPool;
    private List<RankElement> activeElementPool;

    public List<RankElement> ActiveElementPool => activeElementPool;

    public bool IsPoolReady { get; private set; } = false;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        elementPool = new(NetworkManager.MAX_RANK_COUNT);
        activeElementPool = new(NetworkManager.MAX_RANK_COUNT);

        //TODO : Switch to Pool
        for (int i = 0; i < NetworkManager.MAX_RANK_COUNT; i++)
        {
            GameObject obj = Instantiate(rankElementObj.gameObject, this.transform);
            obj.name = (i + 1).ToString();
            obj.SetActive(false);
            elementPool.Enqueue(obj.GetComponent<RankElement>());
        }
    }

    private void CreateElement()
    {
        GameObject obj = Instantiate(rankElementObj.gameObject, this.transform);
        obj.SetActive(false);
        elementPool.Enqueue(obj.GetComponent<RankElement>());
    }

    public RankElement GetRankElement(Transform attachPoint)
    {
        if (elementPool.TryDequeue(out RankElement element))
        {
            activeElementPool.Add(element);
            element.transform.SetParent(attachPoint);
            element.transform.localScale = Vector3.one;
        }
        else
        {
            CreateElement();
            return GetRankElement(attachPoint);
        }
        return element;
    }

    public void ReleaseRankElement(RankElement uselessElement)
    {
        uselessElement.gameObject.SetActive(false);
        activeElementPool.Remove(uselessElement);
        elementPool.Enqueue(uselessElement);
    }

    public void ReleaseAllElements()
    {
        while(activeElementPool.Count > 0)
        {
            activeElementPool[0].gameObject.SetActive(false);
            elementPool.Enqueue(activeElementPool[0]);
            activeElementPool.RemoveAt(0);
        }
    }
}
