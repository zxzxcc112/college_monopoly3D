using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusView : MonoBehaviour
{
    public move move;

    private Color maskColor;  //表示非當前玩家
    private int nowPlayerIndex;  //當前玩家編號
    private int playerCount;  //玩家數
    private int roundCount;    //回合數

    //playerState
    public Transform playerState;
    public List<Image> playerFrame;  //playerState下的玩家外框
    public List<Text> playerHoldingAmount; //playerState下的玩家持有金錢
    public List<Image> playerAvatar;

    //GameState
    public Transform gameState;
    public Text round;
    public Text dicePoint;


    // Start is called before the first frame update
    void Start()
    {
        move = transform.GetComponent<move>();

        maskColor = Color.white;
        maskColor.a = 0.3921569f;
        nowPlayerIndex = move.round;
        playerCount = MyGameManager.instance.people;
        roundCount = 0;
        
        //取得PlayerState外觀
        playerState = GameObject.Find("PlayerState").transform;
        foreach(Transform child in playerState)
        {
            playerFrame.Add(child.GetComponent<Image>());
            foreach(Transform child2 in child)
            {
                if (child2.name == "HoldingAmount")
                {
                    playerHoldingAmount.Add(child2.GetComponent<Text>());
                }
                else if (child2.name == "Avatar")
                {
                    playerAvatar.Add(child2.GetComponent<Image>());
                }
            }
        }

        for (int i = 0; i < playerCount;i++)
        {
            playerAvatar[i].sprite = MyGameManager.instance.roleAvatar[MyGameManager.instance.playerSelectedRole[i]];
        }

        //關閉多餘玩家
        for (int i = playerCount;i < 4;i++)
        {
            playerFrame[i].gameObject.SetActive(false);
        }

        //取得GameState外觀
        gameState = GameObject.Find("GameState").transform;
        round = gameState.GetChild(0).GetChild(1).GetComponent<Text>();
        dicePoint = gameState.GetChild(1).GetChild(1).GetComponent<Text>();

        AddRound();
    }

    // Update is called once per frame
    void Update()
    {
        nowPlayerIndex = move.round;
        PlayerState();
    }

    private void PlayerState()
    {
        for (int i = 0; i < playerCount; i++)
        {
            //更新玩家狀態
            if (i == nowPlayerIndex)
            {
                playerFrame[i].color = Color.white;
            }
            else
            {
                playerFrame[i].color = maskColor;
            }

            //更新玩家持有金錢
            playerHoldingAmount[i].text = move.player[i].player_money.ToString();
            MyGameManager.instance.totalAmount[i] = move.player[i].player_money;
        }
    }

    public void AddRound()
    {
        if (move.round == 0)
        {
            roundCount++;
        }
        round.text = roundCount.ToString();
    }

    public void DiceValue(int diceP)
    {
        dicePoint.text = diceP.ToString();
    }

}
