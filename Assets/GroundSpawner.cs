using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject ground = null; //Prefab

    private float width = 0f;
    private float height = -5.5f;

    private void Awake()
    {

        width = ground.GetComponent<BoxCollider2D>().bounds.size.x / Screen.width;
        Vector3 tmpePos = Camera.main.ScreenToWorldPoint(new Vector3(width, 0, 0f));

        for (int i = 0; i < 10; i++)
        {
            GameObject gr = Instantiate(ground, transform);
            gr.transform.position = new Vector3(tmpePos.x, height, 0f);
        }
    }
}
