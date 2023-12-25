using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_map : MonoBehaviour
{
    public GameObject minimap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        map();
    }

    //滑鼠按住右鍵操作小地圖
    public void map()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                minimap_right();
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                minimap_left();
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                minimap_up();
            }
            if (Input.GetAxis("Mouse Y") < 0)
            {
                minimap_down();
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && minimap.GetComponent<Camera>().orthographicSize < 120)
            {
                minimap_small();
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && minimap.GetComponent<Camera>().orthographicSize > 5)
            {
                minimap_big();
            }
        }
    }

    //小地圖放大
    public void minimap_big()
    {
        minimap.GetComponent<Camera>().orthographicSize -= 5;
    }

    //小地圖縮小
    public void minimap_small()
    {
        minimap.GetComponent<Camera>().orthographicSize += 5;
    }

    //小地圖往上
    public void minimap_up()
    {
        Vector3 v = minimap.transform.localPosition;
        minimap.transform.localPosition = new Vector3(v.x, v.y, v.z + 10);
    }

    //小地圖往左
    public void minimap_left()
    {
        Vector3 v = minimap.transform.localPosition;
        minimap.transform.localPosition = new Vector3(v.x - 10, v.y, v.z);
    }

    //小地圖往右
    public void minimap_right()
    {
        Vector3 v = minimap.transform.localPosition;
        minimap.transform.localPosition = new Vector3(v.x + 10, v.y, v.z);
    }

    //小地圖往下
    public void minimap_down()
    {
        Vector3 v = minimap.transform.localPosition;
        minimap.transform.localPosition = new Vector3(v.x, v.y, v.z - 10);
    }
}
