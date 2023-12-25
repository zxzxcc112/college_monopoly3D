using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnable : MonoBehaviour
{
    //測試用訊息

    public virtual void PrintCorrect()
    {
        Debug.Log("correct");
    }

    public virtual void PrintMessage(string message)
    {
        Debug.Log(message);
    }
}
