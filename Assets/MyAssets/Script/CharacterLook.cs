using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLook : MonoBehaviour
{
    //角色看向下一格
    //用來代表角色正面

    public Transform target;
    public Transform target2;
    public move move;
    public int nextCell;
    public int Id;

    private void Start()
    {


    }

    private void FixedUpdate()
    {
        nextCell = (move.player[Id].now_loc + 1) % 53;
        target = move.cell[nextCell].transform.GetChild(0).transform;
        target2 = move.cell[((nextCell + 1) % 53)].transform.GetChild(0).transform;


        //因為有抽搐所以在接近target時看向下一個
        if (Vector3.Distance(transform.position, target.position) < 1.4f)
        {
            transform.LookAt(target2);
        }
        else
        {
            transform.LookAt(target);
        }      
    }
}
