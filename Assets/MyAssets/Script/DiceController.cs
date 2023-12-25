using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    //管理擲骰空間
    public Transform rollDiceField;

    //管理擲骰動作

    public int diceNumber;  //骰子數
    public int dicePoint;   //所有骰子擲出的點數
    public bool dicesIsSleep; //所有骰子是否靜止
    public List<GameObject> diceList;  //紀錄骰子物件
    public GameObject dice;  //取得骰子的prefab
    public Transform throwPos;  //取得骰子擲出的生成位置
    public GameObject canvas;  //取得畫面上的canvas

    public GameStatusView GSV;  //將點數顯示到介面上
    public move move;
    public GameObject InputField;

    private void Start()
    {
        rollDiceField = GameObject.Find("RollDiceCube").GetComponent<Transform>();

        diceNumber = MyGameManager.instance.diceNumber;
        dicePoint = 0;
        dicesIsSleep = false;
        canvas = GameObject.Find("Canvas").gameObject;
        throwPos = GameObject.Find("ThrowPos").GetComponent<Transform>();
        GSV = transform.GetComponent<GameStatusView>();
        move = transform.GetComponent<move>();
        SetDice();
    }

    //取得prefab位置
    public void SetDice()
    {
        dice = Resources.Load("Prefab/dice") as GameObject;
    }

    //從設定中取得要擲出的骰子數
    public void CountDices()
    {
        diceNumber = MyGameManager.instance.diceNumber;
    }

    //從輸入欄中輸入移動步數
    public void InputDicePoint()
    {
        diceNumber = Int32.Parse(canvas.transform.Find("InputDiceNumber").GetChild(2).GetComponent<Text>().text);
        dicePoint = diceNumber;
    }

    //按下button生成骰子
    public void SpawnDice()
    {

        if (InputField.activeSelf)
        {
            InputDicePoint();
            GSV.DiceValue(dicePoint);
            move.player[move.round].next_loc += dicePoint;
        }
        else
        {
            MoveField();
            CountDices();
            ThrowDice(dice, diceNumber);
            StartCoroutine("CountScore");
        }
    }

    //擲出骰子
    public void ThrowDice(GameObject dice, int times)
    {
        for (int i = 0;i < times;i++)
        {
            GameObject currentDice;
            Rigidbody rigidbody;
            currentDice = Instantiate(dice, throwPos.position, Quaternion.identity) as GameObject;
            //力
            Vector3 force = new Vector3(UnityEngine.Random.Range(100000f, 200000f),
                                          UnityEngine.Random.Range(-200000f, -100000f),
                                          UnityEngine.Random.Range(100000f, 200000f));
            //扭力
            Vector3 torque = new Vector3(UnityEngine.Random.Range(-200000f, 200000f),
                                          UnityEngine.Random.Range(-200000f, 200000f),
                                          UnityEngine.Random.Range(-200000f, 200000f));
            //用隨機力道對骰子的rigidbody施力
            rigidbody = currentDice.GetComponent<Rigidbody>();
            rigidbody.AddForce(force);
            rigidbody.AddTorque(torque);
            diceList.Add(currentDice);
        }
    }

    //當所有骰子停止時將加總點數顯示
    IEnumerator CountScore()
    {
        while(!dicesIsSleep)
        {
            dicesIsSleep = CheckDice();
            yield return null;
        }

        //canvas.transform.Find("GameState/DicePoint").GetChild(1).GetComponent<Text>().text = dicePoint.ToString();
        GSV.DiceValue(dicePoint);
        move.player[move.round].next_loc += dicePoint;

        yield return new WaitForSeconds(2.0f);
        DestroyDices();
        dicePoint = 0;
        GSV.DiceValue(dicePoint);
        dicesIsSleep = false;
    }

    //確認所有骰子是否靜止
    private bool CheckDice()
    {
        for (int i = 0; i < diceList.Count; i++)
        {
            if (!diceList[i].GetComponent<Rigidbody>().IsSleeping())
            {
                return false;
            }
        }
        return true;
    }

    //清除骰子物件
    private void DestroyDices()
    {
        for (int i = 0; i < diceList.Count; i++)
        {
            Destroy(diceList[i]);
        }
        diceList.Clear();
    }

    private void MoveField()
    {
        Transform target = move.player[move.round].play_er.transform;

        rollDiceField.localPosition = target.transform.GetChild(2).transform.position;
        rollDiceField.localPosition += Vector3.down * 5;
    }
}
