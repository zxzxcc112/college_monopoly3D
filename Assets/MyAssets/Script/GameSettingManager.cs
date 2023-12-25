using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingManager : MonoBehaviour
{
    //取得玩家選擇角色區域
    private GameObject[] characterSelections;
    //顯示玩家當前選擇的角色
    private Image[] roleImages;
    private Text[] roleNames;
    private Transform[] lockMask;

    //玩家選擇的角色的編號
    public int[] playerSelected;

    // Start is called before the first frame update
    void Start()
    {
        characterSelections = GameObject.FindGameObjectsWithTag("CharacterSelection");
        roleImages = new Image[characterSelections.Length];
        roleNames = new Text[characterSelections.Length];
        lockMask = new Transform[characterSelections.Length];
        playerSelected = new int[characterSelections.Length];
        for (int i = 0;i < characterSelections.Length;i++)
        {
            playerSelected[i] = MyGameManager.instance.playerSelectedRole[i];
            roleImages[i] = characterSelections[i].transform.Find("CharImage").GetComponent<Image>();
            roleNames[i] = characterSelections[i].transform.Find("NameImage").GetComponentInChildren<Text>();
            lockMask[i] = characterSelections[i].transform.Find("DisablePanel").GetComponent<Transform>();
            ChangeView(i);
        }
        LockCharacterSelections(MyGameManager.instance.people);
    }

    //按下向右按鍵選擇下一個角色
    public void AddIndex(int playerIndex)
    {
        playerSelected[playerIndex] = (playerSelected[playerIndex] + 1) % 4;
        MyGameManager.instance.playerSelectedRole[playerIndex] = playerSelected[playerIndex];
        ChangeView(playerIndex);
    }

    //按下向左按鍵選擇上一個角色(player i)
    public void SubIndex(int playerIndex)
    {
        playerSelected[playerIndex] = playerSelected[playerIndex] - 1;
        if (playerSelected[playerIndex] < 0)
        {
            playerSelected[playerIndex] = 3;
        }
        MyGameManager.instance.playerSelectedRole[playerIndex] = playerSelected[playerIndex];
        ChangeView(playerIndex);
    }

    //切換選擇的角色照片與名字(player i)
    public void ChangeView(int playerIndex)
    {
        roleImages[playerIndex].sprite = MyGameManager.instance.roleImage[playerSelected[playerIndex]];
        roleNames[playerIndex].text = MyGameManager.instance.roleName[playerSelected[playerIndex]];
    }

    //將Dropdown的值(人數,起始金額,骰子數)傳給設定檔
    public void DropdownValueChanged(GameObject gameObject)
    {
        Dropdown dropdown = gameObject.GetComponent<Dropdown>();
        
        if (dropdown != null)
        {
            switch(gameObject.name)
            {
                case "People":
                    MyGameManager.instance.people = int.Parse(dropdown.captionText.text);
                    LockCharacterSelections(MyGameManager.instance.people);
                    break;
                case "StartingAmount":
                    MyGameManager.instance.startingAmount = int.Parse(dropdown.captionText.text.Replace(",",""));
                    break;
                case "DiceNumber":
                    MyGameManager.instance.diceNumber = int.Parse(dropdown.captionText.text);
                    break;
            }
        }
    }

    //根據人數開放/禁用選擇區域(人數)
    private void LockCharacterSelections(int count)
    {
        for (int i = 0;i < count;i++)
        {
            lockMask[i].localScale = new Vector3(0, 0, 0);
        }
        for (int i = count;i < 4; i++)
        {
            lockMask[i].localScale = new Vector3(1, 1, 1);
        }
    }
}
