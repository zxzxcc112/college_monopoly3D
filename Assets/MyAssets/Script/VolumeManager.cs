using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    //BGM音量設定
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //slider = GameObject.Find("Volume/Slider").GetComponent<Slider>();
        slider.value = MyGameManager.instance.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStatic()
    {
        MyGameManager.instance.volume = slider.value;
        MyGameManager.instance.audioSource.volume = slider.value;
    }
}
