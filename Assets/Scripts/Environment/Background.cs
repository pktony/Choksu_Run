using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Background : MonoBehaviour
{
    public Transform[] backgrounds;
    public float speed = 1f;

    private SpriteRenderer _renderer = null;

    private float size_x;


    private void Awake()
    {
        _renderer = backgrounds[0].GetComponent<SpriteRenderer>();

        size_x = Camera.main.pixelWidth * 0.01f;
    }


    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;

            if (backgrounds[i].position.x < -size_x)
            {
                backgrounds[i].position = size_x * 2f * Vector3.right;
            }
        }
    }
}
