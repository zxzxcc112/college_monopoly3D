using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EnablePanel(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void DisablePanel(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
