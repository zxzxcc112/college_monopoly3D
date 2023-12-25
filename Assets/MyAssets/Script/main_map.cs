using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_map : MonoBehaviour
{
    public GameObject mainmap;
    public static GameObject target;
    public float speed;
    public Vector3 cameraPosition;//相機要移動的位置
    public float number;
    public float radius;//移動的半徑

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition.y = 100;
        transform.position = cameraPosition;
        //transform.LookAt(target.transform.position);
        //計算當前攝影機和目標物件的半徑
        radius = 12;
    }

    // Update is called once per frame
    void Update()
    {
        number = speed * 0.01f;
        //target = move.player_[0];
        //計算並設定新的x和y軸位置
        //負數是順時針旋轉，正數是逆時針旋轉
        //cameraPosition.x = target.transform.position.x+ radius * Mathf.Cos(-number);
        //cameraPosition.z = target.transform.position.z+ radius * Mathf.Sin(-number);
        //transform.position = cameraPosition;

        //使相機永遠面對著目標物件
        transform.LookAt(target.transform.position);

        if (Input.GetMouseButton(0))
        {

            if (Input.GetAxis("Mouse X") > 0)
            {
                cameraPosition.x = target.transform.position.x + radius * Mathf.Cos(-number);
                cameraPosition.z = target.transform.position.z + radius * Mathf.Sin(-number);
                transform.position = cameraPosition;
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                cameraPosition.x = target.transform.position.x + radius * Mathf.Cos(number);
                cameraPosition.z = target.transform.position.z + radius * Mathf.Sin(number);
                transform.position = cameraPosition;
            }
        }
    }
}
