using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    //按鍵功能可以從UI中輸入新按鍵來改變預設的按鍵
    //按鍵設定會記錄到靜態class (MyGameManager) 中

    Transform keySettingPanel; //取得按鍵設定介面
    Event keyEvent;    //GUI觸發按鍵事件
    Text buttonText;   //取得button下的text
    KeyCode newKey;    //被輸入的新按鍵

    public static bool waittingForKey;  //判斷是否正在等待輸入新按鍵
    GameObject dialog;  //正在等待輸入新按鍵的對話框

    // Start is called before the first frame update
    void Start()
    {
        keySettingPanel = transform.Find("Layout");
        waittingForKey = false;
        dialog = GameObject.Find("EnterKeyDialog");
        dialog.SetActive(waittingForKey);

        //取得button下text並將文字顯示預設按鍵
        for (int i = 0;i < 5;i++)
        {
            Text text = keySettingPanel.Find("Key" + i.ToString()).GetComponentInChildren<Button>().GetComponentInChildren<Text>();
            switch(i)
            {
                case 0: text.text = MyGameManager.instance.pauseMenu.ToString(); break;
                case 1: text.text = MyGameManager.instance.rollDice.ToString(); break;
                case 2: text.text = MyGameManager.instance.miniMap3D.ToString(); break;
                case 3: text.text = MyGameManager.instance.playerInfo.ToString(); break;
                case 4: text.text = MyGameManager.instance.backDoor.ToString(); break;
                default: break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //執行續優先於按鍵、滑鼠事件
    //取得新按鍵
    private void OnGUI()
    {
        keyEvent = Event.current;
        
        if (keyEvent.isKey && waittingForKey)
        {
            newKey = keyEvent.keyCode;
            waittingForKey = false;
            dialog.SetActive(waittingForKey);
        }
    }

    //按下button進行按鍵設定
    public void StartAssignment(string keyName)
    {
        if (!waittingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    //按下button取得text
    public void SendText(Text text)
    {
        buttonText = text;
    }

    //等待使用者輸入新按鍵
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    //將新按鍵設定到MyGamemanager中
    public IEnumerator AssignKey(string keyName)
    {
        //等待使用者輸入新按鍵
        waittingForKey = true;
        dialog.SetActive(waittingForKey);

        yield return WaitForKey(); 

        //設定新按鍵
        switch (keyName)
        {
            case "menu":
                MyGameManager.instance.pauseMenu = newKey;
                buttonText.text = MyGameManager.instance.pauseMenu.ToString();
                break;
            case "rollDice":
                MyGameManager.instance.rollDice = newKey;
                buttonText.text = MyGameManager.instance.rollDice.ToString();
                break;
            case "miniMap3D":
                MyGameManager.instance.miniMap3D = newKey;
                buttonText.text = MyGameManager.instance.miniMap3D.ToString();
                break;
            case "playerInfo":
                MyGameManager.instance.playerInfo = newKey;
                buttonText.text = MyGameManager.instance.playerInfo.ToString();
                break;
            case "backDoor":
                MyGameManager.instance.backDoor = newKey;
                buttonText.text = MyGameManager.instance.backDoor.ToString();
                break;
            default: break;
        }

        yield return null;
    }
}
