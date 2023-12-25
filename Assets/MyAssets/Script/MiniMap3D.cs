using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap3D : MonoBehaviour
{
    public bool cilck;   //反轉按下狀態
    public float time;   //每幀秒數
    public Camera _Camera;    //取得小地圖攝影機
    private float x;     //小地圖起始位置x
    private float y;     //小地圖起始位置y

    private const float maxSize = 0;     //小地圖位置為0時 小地圖最大
    private const float one = 1;
    private const float miniSize = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        cilck = false;
        _Camera = GameObject.Find("MiniMapCamera3D").GetComponent<Camera>();
        time = Time.deltaTime;
        x = 0.7f;
        y = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        //讓小地圖根據幀數縮放
        if (cilck)
        {
            _Camera.enabled = true ;
            x -= time;
            y -= time;
            if (x < maxSize || y < maxSize)
            {
                x = maxSize;
                y = maxSize;
            }
            _Camera.rect = new Rect(new Vector2(x, y), new Vector2(one, one));
        }
        else
        {
            x += time;
            y += time;
            if (x > miniSize || y > miniSize)
            {
                x = miniSize;
                y = miniSize;
                _Camera.enabled = false;
            }
            _Camera.rect = new Rect(new Vector2(x, y), new Vector2(one, one));
        }
    }

    //按下按鈕反轉狀態
    public void cameraCilck()
    {
        cilck = !cilck;
    }
}
