using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankView : MonoBehaviour
{
    private int people;
    private int[] totalAmount;
    private int[] playerSelectedRole;
    private int[] playerId;

    private Animator animator;

    private List<Transform> rankBox;

    // Start is called before the first frame update
    void Awake()
    {
        people = MyGameManager.instance.people;
        totalAmount = MyGameManager.instance.totalAmount;
        playerSelectedRole = MyGameManager.instance.playerSelectedRole;
        playerId = new int[4];
        for (int i = 0;i < 4;i++)
        {
            playerId[i] = i;
        }

        rankBox = new List<Transform>();
        foreach(Transform box in transform)
        {
            rankBox.Add(box);
        }

        Sort();

        for (int i = 0;i < people;i++)
        {
            rankBox[i].GetChild(1).GetComponent<Text>().text = "P" + (playerId[i]+1);
            rankBox[i].GetChild(2).GetComponent<Text>().text = MyGameManager.instance.roleName[playerSelectedRole[i]].ToString();
            rankBox[i].GetChild(3).GetComponent<Text>().text = totalAmount[i].ToString();
        }

        animator = GetComponent<Animator>();
        animator.SetInteger("PlayerNumber", people);
    }

    private void Sort()
    {
        for (int i = 0;i < people-1;i++)
        {
            for (int j = i+1;j < people;j++)
            {
                if (totalAmount[i] < totalAmount[j])
                {
                    int temp = totalAmount[i];
                    totalAmount[i] = totalAmount[j];
                    totalAmount[j] = temp;

                    temp = playerSelectedRole[i];
                    playerSelectedRole[i] = playerSelectedRole[j];
                    playerSelectedRole[j] = temp;

                    temp = playerId[i];
                    playerId[i] = playerId[j];
                    playerId[j] = temp;
                }
            }
        }
    }
}
