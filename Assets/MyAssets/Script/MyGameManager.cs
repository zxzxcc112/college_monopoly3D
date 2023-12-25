using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager instance;

    //全遊戲設定
    
    public string[] roleName;
    public string[] roleObjectName;
    public Sprite[] roleImage;
    public Sprite[] roleAvatar;
    public Material[] MiniMapMark;
    public Material[] BelongMark;

    //按鍵設定
    public KeyCode pauseMenu;
    public KeyCode rollDice;
    public KeyCode miniMap3D;
    public KeyCode playerInfo;
    public KeyCode backDoor;

    //音效設定
    public float volume;
    public AudioSource audioSource;
    public AudioClip BGM, mouseClick, diceCollision;

    //一局遊戲設定
    public int people;
    public int startingAmount;
    public int diceNumber;
    public int[] playerSelectedRole;
    public int[] totalAmount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            Debug.Log("not destroy");
            
            pauseMenu = KeyCode.Escape;
            rollDice = KeyCode.Space;
            miniMap3D = KeyCode.M;
            playerInfo = KeyCode.N;
            backDoor = KeyCode.B;

            volume = 0.05f;
            audioSource.volume = volume;
            
            roleName = new string[4];
            roleName[0] = "法海";
            roleName[1] = "許仙";
            roleName[2] = "白素貞";
            roleName[3] = "小青";

            roleObjectName = new string[4];
            roleObjectName[0] = "Fahai";
            roleObjectName[1] = "Xu_Xian";
            roleObjectName[2] = "Bai_Suzhen";
            roleObjectName[3] = "Xiaoqing";

            playerSelectedRole = new int[4];
            totalAmount = new int[4];
            ResetOneGameDate();
        }
        else if (this != instance)
        {
            Debug.Log("destroy");
            Destroy(gameObject);
        }
    }

    public void ResetOneGameDate()
    {
        people = 4;
        startingAmount = 10000;
        diceNumber = 1;

        for (int i = 0; i < 4; i++)
        {
            playerSelectedRole[i] = i;
            totalAmount[i] = startingAmount;
        }
    }
}
