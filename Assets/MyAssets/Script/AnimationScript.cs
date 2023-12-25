using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScript : MonoBehaviour
{
    //載入新場景時Loading...的動畫event
    public void SetString()
    {
        this.GetComponent<Text>().text += ".";
    }

    public void ResetString()
    {
        this.GetComponent<Text>().text = "Loading";
    }
}
