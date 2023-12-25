using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDice : MonoBehaviour
{
    //單個骰子的行為

    public Rigidbody diceRB;  //判斷骰子是否停下來
    public Transform[] transforms;  //紀錄骰子的六個面
    public string onTheTop;  //判斷骰子的最上方的數字
    public bool hasShowNum;  //是否已經顯示點數
    public DiceController diceController; //取得骰子管理器,將點數加總到管理器中

    private void Awake()
    {
        diceRB = this.GetComponent<Rigidbody>();
        transforms = new Transform[6];
        onTheTop = null;
        hasShowNum = false;
        //Destroy(this.gameObject, 10.0f);
        GetEveryNum();
        diceController = GameObject.Find("GameManager").GetComponent<DiceController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(diceRB.IsSleeping() && hasShowNum == false)
        {
            GetDiceNumber();
        }
    }

    //取得骰子的六個面
    public void GetEveryNum()
    {
        for (int i = 0;i < 6;i++)
        {
            transforms[i] = this.transform.GetChild(i);
        }
    }

    public void GetDiceNumber()
    {
        //尋找最上方的面(即骰子點數)
        Transform maxiY = transforms[0];
        for (int i = 1;i < 6;i++)
        {
            if (transforms[i].position.y > maxiY.position.y)
                maxiY = transforms[i];
        }
        hasShowNum = true;
        diceController.dicePoint += Int32.Parse(maxiY.name.Replace("side", ""));
    }

    //骰子碰撞的音效
    private void OnCollisionEnter(Collision collision)
    {
        MyGameManager.instance.audioSource.PlayOneShot(MyGameManager.instance.diceCollision);
    }
}
