using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;

    public GameObject pauseMenu;
    public GameObject SettingMenu;

    private void Awake()
    {
        GameIsPaused = false;
        pauseMenu.SetActive(false);
        SettingMenu.SetActive(false);
    }

    public void PauseGame()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        ReturnPauseMenu();
        GameIsPaused = true;
    }

    public void ReturnPauseMenu()
    {
        SettingMenu.SetActive(false);
    }

    public void OpenSettingMenu()
    {
        SettingMenu.SetActive(true);
    }
}
