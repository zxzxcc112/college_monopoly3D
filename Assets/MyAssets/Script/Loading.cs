using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    private GameObject loadingScreen;
    private Text loadText;
    private Slider loadBar;

    // Start is called before the first frame update
    void Start()
    {
        loadingScreen = GameObject.Find("LoadingScreen");
        loadText = loadingScreen.transform.Find("LoadText").GetComponent<Text>();
        loadBar = loadingScreen.transform.Find("LoadBar").GetComponent<Slider>();
        loadingScreen.transform.localScale = new Vector3(0, 0, 0);
    }

    public void StartClick()
    {
        loadingScreen.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(DisplayLoadingScreen("StartGamePlay"));
    }

    IEnumerator DisplayLoadingScreen(string scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while(!async.isDone)
        {
            if (async.progress < 0.9f)
            {
                loadText.text = (int)(async.progress * 100) + "%";
                loadBar.value = async.progress;
            }
            else
            {
                loadText.text = "100%";
                loadBar.value = 1;
            }
            Debug.Log("test");
            yield return null;
        }
        
    }
}
