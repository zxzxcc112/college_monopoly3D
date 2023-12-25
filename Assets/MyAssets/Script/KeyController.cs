using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    //按下按鍵後執行各種行為
    ScenesManager scenesManager;
    PauseMenu pauseMenu;
    MiniMap3D miniMap3D;
    DiceController diceController;
    move move;

    private void Start()
    {
        scenesManager = gameObject.GetComponent<ScenesManager>();
        pauseMenu = gameObject.GetComponent<PauseMenu>();
        miniMap3D = gameObject.GetComponent<MiniMap3D>();
        diceController = gameObject.GetComponent<DiceController>();
        move = gameObject.GetComponent<move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(MyGameManager.instance.pauseMenu))
            pauseMenu.PauseGame();
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetKeyDown(MyGameManager.instance.rollDice) && move.button.interactable)
                move.button_on();
            if (Input.GetKeyDown(MyGameManager.instance.miniMap3D))
                miniMap3D.cameraCilck();
            if (Input.GetKeyDown(MyGameManager.instance.playerInfo))
                diceController.InputField.SetActive(!diceController.InputField.activeSelf);
            if (Input.GetKeyDown(MyGameManager.instance.backDoor))
                scenesManager.ChangeScene("GameOver");
        }
    }
}
